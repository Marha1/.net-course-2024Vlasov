using System.Collections.Concurrent;
using System.Text.Json;
using BankSystem.App.Services;
using BankSystemDomain.Models;
using Xunit;

namespace ExportTool.Test;

public class ThreadAndTaskTests
{
    private const int FileSizeLimit = 1024 * 1024;
    private readonly TestDataGenerator _dataGenerator;

    public ThreadAndTaskTests()
    {
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void PipelineProcessing_ShouldWriteClientsToFiles()
    {
        // Arrange
        var clients = _dataGenerator.GenerateClients(500); 
        var directoryPath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\ClientPipeline";
        Directory.CreateDirectory(directoryPath);

        var clientQueue = new BlockingCollection<Client>(new ConcurrentQueue<Client>(clients));
        var fileIndex = 1;
        var filePath = Path.Combine(directoryPath, $"clients_{fileIndex}.json");
        
        File.Create(filePath).Dispose();
        var producerThread = new Thread(() =>
        {
            foreach (var client in clients) 
                clientQueue.Add(client);
            clientQueue.CompleteAdding();
        });
        
        var consumerThread = new Thread(() =>
        {
            while (!clientQueue.IsCompleted)
            {
                if (new FileInfo(filePath).Length >= FileSizeLimit)
                {
                    fileIndex++;
                    filePath = Path.Combine(directoryPath, $"clients_{fileIndex}.json");
                }

                if (clientQueue.TryTake(out var client))
                {
                    var json = JsonSerializer.Serialize(client);
                    File.AppendAllText(filePath, json + Environment.NewLine);
                }
            }
        });

        // Act
        producerThread.Start();
        consumerThread.Start();
        producerThread.Join();
        consumerThread.Join();

        // Assert
        var files = Directory.GetFiles(directoryPath, "clients_*.json");
        Assert.True(files.Length > 0);

        foreach (var file in files)
        {
            var fileContent = File.ReadAllLines(file);
            Assert.True(fileContent.Length > 0); 
        }
    }
    
    [Fact]
    public void ParallelMoneyAddition_ShouldAddExpectedAmount()
    {
        // Arrange
        var account = new Account { Amount = 0 };
        var locker = new object();
        var expectedAmount = 2000;

        var thread1 = new Thread(() => AddMoney(account, locker));
        var thread2 = new Thread(() => AddMoney(account, locker));
    
        // Act
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();

        // Assert
        Assert.Equal(expectedAmount, account.Amount);
    }
    private void AddMoney(Account account, object locker)
    {
        for (var i = 0; i < 10; i++)
        {
            lock (locker)
            {
                account.Amount += 100;
            }
        }
    }

}