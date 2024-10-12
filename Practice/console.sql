CREATE TABLE Clients (
    id SERIAL PRIMARY KEY,
    phone_number VARCHAR(15),
    name VARCHAR(50),
    surname VARCHAR(50),
    passport_details VARCHAR(20),
    age INT,
    birth_date DATE
);

CREATE TABLE Employees (
    id SERIAL PRIMARY KEY,
    phone_number VARCHAR(15),
    name VARCHAR(50),
    surname VARCHAR(50),
    passport_details VARCHAR(20),
    age INT,
    birth_date DATE,
    experience INT,
    salary DECIMAL(10, 2),
    contract VARCHAR(100)
);

CREATE TABLE Currency (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50)
);

CREATE TABLE Accounts (
    id SERIAL PRIMARY KEY,
    client_id INT REFERENCES Clients(id),
    currency_id INT REFERENCES Currency(id),
    amount DECIMAL(15, 2)
);

INSERT INTO Currency (name) VALUES
    ('USD'),
    ('EUR'),
    ('RUB');

INSERT INTO Clients (phone_number, name, surname, passport_details, age, birth_date) VALUES
    ('1234567890', 'Рома', 'что-то', '854123', 30, '1994-01-01'),
    ('0987654321', 'Гена', 'кто-то', '232143', 25, '1998-02-02'),
    ('5556667778', 'Игорь', 'фвфы', '31ds21', 40, '1983-03-03'),
    ('7778889990', 'Вова', 'вфвс2', '88c81x', 20, '2003-04-04');

INSERT INTO Accounts (client_id, currency_id, amount) VALUES
    (1, 1, 1000.00),
    (1, 2, 500.00),
    (2, 1, 300.00),
    (3, 3, 1500.00),
    (4, 2, 250.00),
    (2, 3, 100.00);

SELECT c.name, c.surname, SUM(a.amount) AS total_amount
FROM Clients c
JOIN Accounts a ON c.id = a.client_id
GROUP BY c.id
HAVING SUM(a.amount) < 400
ORDER BY total_amount ASC;

SELECT c.name, c.surname, MIN(a.amount) AS min_amount
FROM Clients c
JOIN Accounts a ON c.id = a.client_id
GROUP BY c.id
ORDER BY min_amount ASC
LIMIT 1;

SELECT SUM(a.amount) AS total_money
FROM Accounts a;

SELECT c.name, c.surname, a.amount, cu.name AS currency
FROM Accounts a
JOIN Clients c ON a.client_id = c.id
JOIN Currency cu ON a.currency_id = cu.id;

SELECT name, surname, age
FROM Clients
ORDER BY age DESC;

SELECT age, COUNT(*) AS count
FROM Clients
GROUP BY age
ORDER BY count DESC;

SELECT age, COUNT(*) AS count
FROM Clients
GROUP BY age;

SELECT *
FROM Clients
LIMIT 3;
