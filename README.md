# Grade Management App

## Overview
This application is a Grade Management system used to manage, process, and store grade-related data. It allows users to create, update, and manage grade records efficiently within a central system.

---

## Data Storage

The application uses a database to persist all grade data when running in a configured environment.

### Production / Connected Mode
If a valid database connection string is provided in the application configuration, the system will connect to the database and all data will be stored permanently.

This ensures:
- Persistent grade storage
- Shared access across users
- Reliable data retention

---

### Development Mode (No Connection String)

If no database connection string is provided, the application will automatically fall back to an **in-memory database**.

In this mode:
- No external database is required
- Data is stored only in memory
- All data will be lost when the application stops

This is intended for:
- Local development
- Testing
- Debugging

---

## Configuration

To connect to a database, add the following to your configuration:

Example:

If this is not provided, the application will run in in-memory mode automatically.

---

## Notes
- In-memory mode is not persistent and should not be used for production.
- Database mode is recommended for all production environments.

