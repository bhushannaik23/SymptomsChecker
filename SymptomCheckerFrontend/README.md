# Symptom Checker Frontend

A comprehensive Angular frontend application for the Symptoms Checker healthcare platform.

## Features

- **Role-based Authentication**: Separate dashboards for Patients, Doctors, and Admins
- **Symptom Analysis**: AI-powered symptom checker with recommendations
- **Appointment Management**: Book and manage medical appointments
- **Medical History**: Track patient medical records and history
- **User Management**: Admin panel for managing users and system settings
- **Responsive Design**: Mobile-friendly interface with Bootstrap 5
- **Modern UI**: Clean, professional healthcare-focused design

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   └── layout/
│   │       ├── header/
│   │       └── footer/
│   ├── core/
│   │   ├── guards/
│   │   ├── interceptors/
│   │   └── services/
│   ├── features/
│   │   ├── auth/
│   │   │   ├── login/
│   │   │   └── register/
│   │   ├── dashboard/
│   │   ├── patient/
│   │   │   ├── symptoms/
│   │   │   ├── appointments/
│   │   │   └── history/
│   │   ├── doctor/
│   │   │   ├── patients/
│   │   │   ├── appointments/
│   │   │   └── analytics/
│   │   ├── admin/
│   │   │   ├── users/
│   │   │   ├── settings/
│   │   │   └── reports/
│   │   └── home/
│   ├── models/
│   ├── shared/
│   └── services/
```

## User Roles & Features

### Patient Features
- **Symptom Checker**: Describe symptoms and get AI-powered analysis
- **Appointment Booking**: Schedule appointments with doctors
- **Medical History**: View past consultations and diagnoses
- **Profile Management**: Update personal information

### Doctor Features
- **Patient Management**: View and manage patient list
- **Appointment Scheduling**: Manage appointment calendar
- **Analytics Dashboard**: View patient statistics and performance metrics
- **Medical Records**: Access patient medical history

### Admin Features
- **User Management**: Manage doctors, patients, and admin users
- **System Settings**: Configure system parameters and security
- **Reports & Analytics**: View system-wide reports and statistics
- **Backup & Maintenance**: System backup and maintenance tools

## Technology Stack

- **Framework**: Angular 18+
- **UI Library**: Bootstrap 5
- **Icons**: Font Awesome 6
- **Styling**: SCSS
- **HTTP Client**: Angular HttpClient
- **Routing**: Angular Router with Guards
- **Forms**: Reactive Forms
- **Authentication**: JWT-based authentication

## Prerequisites

- Node.js (v18 or higher)
- npm (v9 or higher)
- Angular CLI (v18 or higher)

## Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd SymptomCheckerFrontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Install Angular CLI globally (if not already installed)**
   ```bash
   npm install -g @angular/cli
   ```

## Development

1. **Start the development server**
   ```bash
   ng serve
   ```

2. **Navigate to the application**
   Open your browser and go to `http://localhost:4200`

3. **Backend API Configuration**
   - Update the API base URL in `src/app/core/services/api.service.ts`
   - Default backend URL is set to `http://localhost:5202/api`

## Build

1. **Build for production**
   ```bash
   ng build --configuration production
   ```

2. **Build output**
   The build artifacts will be stored in the `dist/` directory.

## Testing

1. **Run unit tests**
   ```bash
   ng test
   ```

2. **Run end-to-end tests**
   ```bash
   ng e2e
   ```

## Authentication Flow

1. **Registration**: Users can register as Patient or Doctor
2. **Login**: JWT-based authentication with role-based routing
3. **Route Guards**: Protected routes based on authentication and user roles
4. **Token Management**: Automatic token handling with HTTP interceptors

## API Integration

The frontend is designed to work with the .NET backend API:

- **Base URL**: `http://localhost:5202/api`
- **Authentication**: Bearer token authentication
- **CORS**: Configured to allow requests from `http://localhost:4200`

### Key API Endpoints

- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/patients` - Get patients (Doctor/Admin)
- `POST /api/appointments` - Create appointment
- `POST /api/symptoms/analyze` - Analyze symptoms

**Note**: Make sure your .NET backend is configured to allow CORS from `http://localhost:4200`

## Deployment

1. **Build the application**
   ```bash
   ng build --configuration production
   ```

2. **Deploy to web server**
   - Copy the contents of `dist/` to your web server
   - Configure server to serve `index.html` for all routes (SPA routing)

3. **Environment Configuration**
   - Update API URLs in environment files
   - Configure HTTPS certificates if needed

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.

## Support

For support and questions, please contact the development team or create an issue in the repository.