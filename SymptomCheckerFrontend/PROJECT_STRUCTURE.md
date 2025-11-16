# Angular Frontend Project Structure

## Overview
This is a comprehensive Angular frontend for the Symptoms Checker healthcare platform with role-based authentication and separate dashboards for Patients, Doctors, and Admins.

## Key Features Implemented

### 1. Authentication System
- **Login Component**: `src/app/features/auth/login/`
- **Register Component**: `src/app/features/auth/register/`
- **Auth Service**: JWT-based authentication with role management
- **Guards**: Route protection based on authentication and roles

### 2. Role-Based Dashboards

#### Patient Features
- **Symptom Checker**: AI-powered symptom analysis
- **Appointments**: Book and manage medical appointments
- **Medical History**: View past consultations and diagnoses

#### Doctor Features
- **Patient Management**: View and manage patient list
- **Appointment Scheduling**: Manage appointment calendar
- **Analytics Dashboard**: View patient statistics and performance metrics

#### Admin Features
- **User Management**: Manage doctors, patients, and admin users
- **System Settings**: Configure system parameters and security
- **Reports & Analytics**: View system-wide reports and statistics

### 3. Layout Components
- **Header**: Navigation with role-based menu items
- **Footer**: Application footer with disclaimer
- **Responsive Design**: Bootstrap 5 integration

## File Structure

```
src/app/
├── components/
│   └── layout/
│       ├── header/
│       │   ├── header.component.html
│       │   ├── header.component.scss
│       │   └── header.component.ts
│       └── footer/
│           ├── footer.component.html
│           ├── footer.component.scss
│           └── footer.component.ts
├── core/
│   ├── guards/
│   │   ├── auth.guard.ts
│   │   └── role.guard.ts
│   ├── interceptors/
│   │   └── auth.interceptor.ts
│   └── services/
│       ├── api.service.ts
│       └── auth.service.ts
├── features/
│   ├── auth/
│   │   ├── login/
│   │   │   ├── login.component.html
│   │   │   ├── login.component.scss
│   │   │   └── login.component.ts
│   │   └── register/
│   │       ├── register.component.html
│   │       ├── register.component.scss
│   │       └── register.component.ts
│   ├── dashboard/
│   │   └── dashboard.component.ts
│   ├── patient/
│   │   ├── symptoms/
│   │   ├── appointments/
│   │   ├── history/
│   │   └── patient.routes.ts
│   ├── doctor/
│   │   ├── patients/
│   │   ├── appointments/
│   │   ├── analytics/
│   │   └── doctor.routes.ts
│   ├── admin/
│   │   ├── users/
│   │   ├── settings/
│   │   ├── reports/
│   │   └── admin.routes.ts
│   └── home/
│       └── home.component.ts
└── models/
    ├── user.model.ts
    ├── patient.model.ts
    ├── doctor.model.ts
    ├── appointment.model.ts
    └── symptom.model.ts
```

## Configuration Updates Made

### 1. API Configuration
- Updated base URL from `https://localhost:7000` to `http://localhost:5202`
- Configured for HTTP instead of HTTPS
- Updated both AuthService and ApiService

### 2. Component Structure
- Separated HTML templates into `.component.html` files
- Separated styles into `.component.scss` files
- Kept logic in `.component.ts` files
- Updated component decorators to use `templateUrl` and `styleUrls`

### 3. Routing Configuration
- Implemented lazy loading for feature modules
- Role-based route guards
- Protected routes for authenticated users only

### 4. Styling
- Bootstrap 5 integration
- Font Awesome icons
- Custom SCSS styles
- Responsive design

## Next Steps

1. **Start the Development Server**:
   ```bash
   cd SymptomCheckerFrontend
   ng serve
   ```

2. **Ensure Backend is Running**:
   - Make sure your .NET API is running on `http://localhost:5202`
   - Verify CORS is configured to allow `http://localhost:4200`

3. **Test the Application**:
   - Navigate to `http://localhost:4200`
   - Test registration and login functionality
   - Verify role-based navigation works correctly

## Important Notes

- All components use standalone components (Angular 17+ feature)
- JWT tokens are stored in localStorage
- Role-based routing is implemented with guards
- The application is fully responsive and mobile-friendly
- All API calls are configured for your backend port (5202)

## Troubleshooting

If you encounter any issues:

1. **CORS Errors**: Ensure your .NET backend allows CORS from `http://localhost:4200`
2. **API Connection**: Verify your backend is running on port 5202
3. **Authentication Issues**: Check JWT token configuration in both frontend and backend
4. **Routing Issues**: Ensure all route guards are properly configured

The application is now ready for development and testing with your .NET backend!