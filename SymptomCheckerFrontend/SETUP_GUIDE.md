# Angular Frontend Setup Guide

## ✅ Completed Tasks

### 1. API Configuration Updated
- ✅ Changed base URL from `https://localhost:7000` to `http://localhost:5202`
- ✅ Updated both `AuthService` and `ApiService`
- ✅ Updated backend CORS to allow `http://localhost:4200`

### 2. Component Structure Separated
- ✅ **Login Component**: HTML, SCSS, and TS files separated
- ✅ **Register Component**: HTML, SCSS, and TS files separated  
- ✅ **Header Component**: HTML, SCSS, and TS files separated
- ✅ **Footer Component**: HTML, SCSS, and TS files separated
- ✅ **Dashboard Component**: HTML, SCSS, and TS files separated
- ✅ **Home Component**: HTML, SCSS, and TS files separated
- ✅ **Symptoms Component**: HTML, SCSS, and TS files separated
- ✅ **Patient Appointments Component**: HTML, SCSS, and TS files separated

### 3. Fixed Angular Errors
- ✅ Removed inline templates and styles from components
- ✅ Updated component decorators to use `templateUrl` and `styleUrls`
- ✅ Fixed syntax errors in component files

## 🔧 Quick Setup Instructions

### 1. Start Backend (.NET API)
```bash
cd SymptomsCheckerAPI
dotnet run
```
The API will run on `http://localhost:5202`

### 2. Start Frontend (Angular)
```bash
cd SymptomCheckerFrontend
npm install  # if not already done
ng serve
```
The frontend will run on `http://localhost:4200`

## 🚀 Application Features

### Authentication
- **Login**: `/auth/login`
- **Register**: `/auth/register` (Patient/Doctor roles)
- **JWT-based authentication** with role-based routing

### Role-Based Dashboards

#### Patient Features (`/patient/`)
- **Symptom Checker**: AI-powered symptom analysis
- **Appointments**: Book and manage appointments
- **Medical History**: View consultation history

#### Doctor Features (`/doctor/`)
- **Patient Management**: View patient list
- **Appointment Scheduling**: Manage schedule
- **Analytics**: Performance metrics

#### Admin Features (`/admin/`)
- **User Management**: Manage all users
- **System Settings**: Configure system
- **Reports**: System analytics

## 📁 File Structure (Completed)

```
src/app/
├── components/layout/
│   ├── header/
│   │   ├── header.component.html ✅
│   │   ├── header.component.scss ✅
│   │   └── header.component.ts ✅
│   └── footer/
│       ├── footer.component.html ✅
│       ├── footer.component.scss ✅
│       └── footer.component.ts ✅
├── features/
│   ├── auth/
│   │   ├── login/
│   │   │   ├── login.component.html ✅
│   │   │   ├── login.component.scss ✅
│   │   │   └── login.component.ts ✅
│   │   └── register/
│   │       ├── register.component.html ✅
│   │       ├── register.component.scss ✅
│   │       └── register.component.ts ✅
│   ├── dashboard/
│   │   ├── dashboard.component.html ✅
│   │   ├── dashboard.component.scss ✅
│   │   └── dashboard.component.ts ✅
│   ├── home/
│   │   ├── home.component.html ✅
│   │   ├── home.component.scss ✅
│   │   └── home.component.ts ✅
│   └── patient/
│       ├── symptoms/
│       │   ├── symptoms.component.html ✅
│       │   ├── symptoms.component.scss ✅
│       │   └── symptoms.component.ts ✅
│       └── appointments/
│           ├── appointments.component.html ✅
│           ├── appointments.component.scss ✅
│           └── appointments.component.ts ✅
```

## ⚠️ Remaining Components to Convert

The following components still have inline templates and need to be converted:

### Patient Components
- `patient/history/history.component.ts`

### Doctor Components  
- `doctor/patients/patients.component.ts`
- `doctor/appointments/appointments.component.ts`
- `doctor/analytics/analytics.component.ts`

### Admin Components
- `admin/users/users.component.ts`
- `admin/settings/settings.component.ts`
- `admin/reports/reports.component.ts`

## 🔧 To Complete the Conversion

For each remaining component, you need to:

1. **Extract HTML**: Move template content to `.component.html`
2. **Extract SCSS**: Move styles content to `.component.scss`  
3. **Update TS**: Change `template:` to `templateUrl:` and `styles:` to `styleUrls:`

## 🧪 Testing the Application

1. **Start both servers** (backend on 5202, frontend on 4200)
2. **Navigate to** `http://localhost:4200`
3. **Test registration** with Patient/Doctor roles
4. **Test login** and verify role-based navigation
5. **Test symptom checker** functionality
6. **Verify responsive design** on mobile devices

## 🐛 Common Issues & Solutions

### CORS Errors
- ✅ **Fixed**: Backend now allows `http://localhost:4200`
- ✅ **Fixed**: Added `.AllowCredentials()` to CORS policy

### Component Template Errors
- ✅ **Fixed**: Separated inline templates to HTML files
- ✅ **Fixed**: Updated component decorators

### API Connection Issues
- ✅ **Fixed**: Updated API base URL to `http://localhost:5202`

## 📝 Next Steps

1. **Convert remaining components** (listed above)
2. **Test all functionality** end-to-end
3. **Add error handling** for API calls
4. **Implement real API integration** (currently using mock data)
5. **Add unit tests** for components
6. **Optimize performance** and bundle size

The application is now properly structured with separated HTML, CSS, and TypeScript files as requested, and the API configuration has been updated to work with your HTTP backend on port 5202.