# Charity Donation Web App

This project is a full-stack web application developed as part of a diploma thesis. It provides a digital platform where users can create and support charitable campaigns by making donations, writing comments, and sharing updates.

## 📌 Project Overview

The application connects people with charitable intentions and those in need. It enables users to:
- Create and manage charitable campaigns
- Donate to causes
- Comment on campaigns
- View updates from campaign owners
- Filter and search campaigns

## 🌐 Live Demo

Check out the live application here: [(https://charity-donation-web-app.onrender.com/)](https://charity-donation-web-app.onrender.com/)]

## 🧰 Technologies Used

**Frontend:**
- HTML, CSS, SCSS
- Bootstrap
- JavaScript

**Backend:**
- C#
- ASP.NET Core MVC
- Entity Framework

**Database:**
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)


## ✨ Key Features

- Role-based user access (Admin, User)
- Campaign creation with approval by admin
- Commenting and updates per campaign
- Donations with optional anonymity
- Filtering campaigns by name or donation status
- Detailed dashboards for campaign progress and donation stats

## 🧪 Use Cases

- Browse all approved campaigns
- Filter/search campaigns
- View campaign details
- Register/Login as a user
- Create a campaign (pending admin approval)
- Admin: Approve or decline campaigns

## 🚀 Getting Started

### 1. Clone the repository  
    git clone https://github.com/GeorgiStoyanov05/DZI-Project.git
    cd DZI-Project/CharityProject
### 2. Setup backend (ASP.NET Core)
- Open CharityProject in Visual Studio

- Run Update-Database from Package Manager Console

- Start the application with IIS Express or dotnet run

### 3. Setup frontend (Client-Side Enhancements)
- Ensure you have Node.js installed (if used for JS packages)

- Use npm install if there’s a package.json for additional frontend tools

### 🔐 Authentication & Authorization
- Identity system with roles

- Admin panel with restricted access

- Users can manage only their own campaigns, comments, and updates

## 📄 License

This project is licensed under the MIT License.
