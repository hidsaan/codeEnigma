CREATE TABLE users (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    firstname VARCHAR (100) NOT NULL,
    lastname VARCHAR (100) NOT NULL,
    dob VARCHAR (50) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR (150) NOT NULL UNIQUE,
    address VARCHAR(100) NULL,
    password VARCHAR (20) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO users (firstname, lastname, dob, phone, email, address, password)
VALUES
('Aiman', 'Mir', '12-09-2000', 'Hawal', 'aiman@gmail.com', '9906599065', '12346qweeeee')
