# Project Name

## Description

This is a C# MVC .NET Core 6 project that uses PostgreSQL as the database. It is designed to be run in a Docker container for easy deployment and management. 

## Table of Contents

- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

## Technologies Used

- C#
- ASP.NET Core 6 MVC
- PostgreSQL
- Docker
- Hangfire
- FluentValidation
- Serilog

## Installation

To run this project, you'll need the following prerequisites:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)

Follow these steps to set up and run the project:

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/EmirBaran-Ozdemir/CRM.git
   ```

2. Navigate to the project directory:

   ```bash
   cd CRM
   ```

3. Configure the PostgreSQL connection string in the `appsettings.json` file.

4. Access the application in your web browser at `https://localhost:7133`.

## Usage

Once the application is running, you can access its features through the web interface. Here are some common actions:

 - Customer Management: Add, edit, or delete customer information.

 - Product/License Management: Record details of products or licenses sold to customers.

 - Usage Limits: Set usage limits for customers and track their usage.

 - Invoice Generation: Invoices for excessive usage are automatically generated at the end of each month.

Authentication and Authorization
 - The application uses Cookie-based authentication. Users can log in and access specific functionalities based on their roles (e.g., admin, buyer, seller).

Authorization policies are defined for different roles, such as "Admin," "Buyer," and "Seller."
## Configuration

Explain any configuration options or settings that users may need to modify in your application.

## Contributing

If you'd like to contribute to this project, please follow these guidelines:

1. Fork the repository on GitHub.
2. Create a new branch with a descriptive name.
3. Make your changes and commit them with clear messages.
4. Push your changes to your fork.
5. Submit a pull request to the original repository.

## License

This project is licensed under the [MIT License](LICENSE).
