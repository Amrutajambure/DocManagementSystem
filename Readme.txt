# 📄 Document Management System (DMS)

This project is a secure backend for uploading, versioning, and retrieving documents with private access for each user. Built using ASP.NET Core Web API and SQL Server.

---

## 🏗️ Tech Stack

- **Backend**: ASP.NET Core Web API (.NET 7+)
- **Database**: SQL Server (file storage as BLOBs)
- **Authentication**: JWT (JSON Web Token)
- **ORM**: Entity Framework Core
- **Testing**: xUnit

---

## 🚀 How to Build and Run the Backend

### 1. Clone the Repository

```bash
git clone  https://github.com/Amrutajambure/DocManagementSystem.git
cd DocManagementSystem


📥 API Endpoints
🔐 AuthController
Method	Endpoint	Description
POST	/api/auth/register	Register a new user
POST	/api/auth/login	Get JWT token

📁 DocumentController
Method           	Endpoint	                                          Description
POST	/api/documents/upload	                         Upload a document or new version
GET	    /api/documents/{fileName}	                     Download the latest version
GET  	/api/documents/{fileName}?revision=X	         Download a specific version by index

All /api/documents endpoints require a valid JWT token.


🤖 AI Tools Used
ChatGPT – code generation and guidance

GitHub Copilot – code auto-completions

