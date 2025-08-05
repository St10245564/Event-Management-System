# 🎪 Event Management System

A modern ASP.NET Core MVC web application that simplifies managing venues, events, and bookings.
Includes support for Azure Blob Storage image uploads, event type seeding, and venue availability validation.

![image1](https://github.com/user-attachments/assets/977a5b8a-a730-4fe6-a639-739a68c4c352)
![image2](https://github.com/user-attachments/assets/7460b7bd-45d7-46cc-a6db-7247aca26bbf)
![image3](https://github.com/user-attachments/assets/3a283f94-a931-4741-9c8a-86df97d1a03a)

---

## 📌 Core Features

- 🏟️ **Venue Management**
  - Create venues with event type selection
  - Upload and store venue images in **Azure Blob Storage**
  - View venue listings with images and types
  - Auto-seed default event types: Conference, Wedding, Concert, Workshop

- 📅 **Event Management**
  - Create events with linked venue and event type
  - View a list of all events

- 🗓️ **Booking Management**
  - Book venues for specific events
  - Prevent double-bookings on the same venue/date
  - Filter bookings by event name, type, date range, and availability

- 📊 **Dashboard Overview**
  - Homepage shows total counts for venues, events, and bookings

---

## 🛠️ Tech Stack

- **ASP.NET Core MVC** – Backend web framework
- **Entity Framework Core** – ORM for database interactions
- **Azure Blob Storage** – Stores venue images securely
- **SQL Server / LocalDB** – Database backend
- **Bootstrap 5** – Responsive and modern UI

---

## 💾 Project Structure

MyPart3/
├── Controllers/
│ ├── HomeController.cs
│ ├── EventController.cs
│ ├── BookingController.cs
│ └── VenueController.cs
│
├── Models/
│ ├── Event.cs
│ ├── Venue.cs
│ ├── Booking.cs
│ └── EventType.cs
│
├── Services/
│ └── IBlobService.cs
│
├── Data/
│ └── ApplicationDbContext.cs
│
├── Views/
│ ├── Home/
│ ├── Event/
│ ├── Booking/
│ └── Venue/
│
├── wwwroot/
│ └── css, js, images
│
├── appsettings.json
├── Program.cs
└── MyPart3.csproj

yaml
Copy
Edit

---

## 🚀 Getting Started

### 📦 Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- Azure Storage Account with a Blob Container

---

### 🧰 Setup Instructions

1. **Clone the repository:**
   ```bash
   https://github.com/St10245564/Event-Management-System.git
   cd EventManagementSystem
Configure appsettings.json:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventDB;Trusted_Connection=True;"
},
"AzureBlobStorage": {
  "ConnectionString": "YOUR_AZURE_BLOB_CONNECTION_STRING",
  "ContainerName": "YOUR_CONTAINER_NAME"
}
Apply EF Core migrations:

bash
Copy
Edit
dotnet ef database update
Run the app:

bash
Copy
Edit
dotnet run
Open your browser and go to: https://localhost:5001

🧪 Testing Features
✅ Try uploading a venue image (Azure Blob upload)

✅ Create events and link to venues and types

✅ Book a venue and test for double-booking prevention

✅ Use search filters on bookings (event type, name, date, availability)

🔐 Security & Validation
Input validation for bookings and events

Image upload validation via Blob service

Server-side error handling with user-friendly messages

📈 Roadmap
🔐 User Authentication and Role-based Access

💳 Payment Integration

📧 Email Confirmations

📊 Analytics Dashboard

📱 Responsive Mobile UX Enhancements

👨‍💻 Author
Your Name
GitHub: https://github.com/St10245564

📄 License
This project is licensed under the MIT License.

🤝 Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss the changes you'd like to make.


