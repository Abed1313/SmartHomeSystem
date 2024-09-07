# Smart Home System

## Overview

The Smart Home System is designed to manage and automate tasks within a smart home environment. Users, including Admins, Guests, and Providers, can interact with various components such as devices, rooms, access controls, and scenes. The system allows centralized control and monitoring, making home automation accessible and customizable.

### Key Features

- User Roles: Admin, Guest, and Provider, each with distinct permissions and functionalities.
- Device Management: Adding and controlling smart devices in different rooms.
- Scene Creation: Setting up scenes that control multiple devices at once.
- Automation Rules: Automating device behavior based on certain conditions.
- Alerts and Notifications: Alerts for device actions or errors, with optional email notifications.

---

## Entity Overview

### Core Entities

1. User (Characters.cs)
    - Inherits from IdentityUser.
    - Shared properties for all user types (Admin, Guest, Provider).
    - Navigation properties to manage access control, houses, rooms, devices, scenes, and more.

2. Admin (Admin.cs)
    - Inherits from User.
    - Can manage Houses, Devices, Rooms, AutomationRules, Alerts, and more.
  
3. Guest (Guest.cs)
    - Inherits from User.
    - Limited access to specific rooms and devices through AccessControl.

4. Provider (Provider.cs)
    - Inherits from User.
    - Offers specific services and has access to manage their related devices or systems.

### System Components

1. House (House.cs)
    - Represents a house in the system.
    - Has multiple Rooms.

2. Room (Room.cs)
    - Contains Devices and can be part of automation rules or scenes.
  
3. Device (Device.cs)
    - Represents any smart device within a room (e.g., lights, thermostats).
    - Related to DeviceType for categorizing devices.
  
4. Scene (Scene.cs)
    - A collection of actions across multiple devices.
    - Linked with SceneAction and AutomationRule to define the device behaviors.

5. AutomationRule (AutomationRule.cs)
    - Defines rules that automate device behavior based on specific conditions.
    - Can be triggered by time, events, or user input.

6. AccessControl (AccessControl.cs)
    - Manages who has access to which rooms, devices, or systems.
  
7. Alert (Alert.cs)
    - Notifications triggered by device actions, system errors, or user-defined events.
    - Can be linked to Notification for sending emails or in-app alerts.

8. EnergyUsage (EnergyUsage.cs)
    - Tracks energy consumption of various devices in the system.

9. SubscriptionPlan (SubscriptionPlan.cs)
    - Handles user subscriptions for access to premium features.

---

## Repository Structure

- Models: Contains all the core entities of the Smart Home System.
- DTO (Data Transfer Objects): Used to transfer data between the client and the server.
- Repository: Handles data access logic.
- Services: Contains business logic for managing different aspects of the system (e.g., alerts, automation, scenes).

---

## How to Use the System

### Scenario: Adding a Smart Device to a Room

1. Login as Admin: Use the provided authentication endpoint to login as an Admin.
   
2. Create a House: 
    - Use the POST request to add a new house:
    ```json
    POST /api/houses
    {
      "name": "Saja's House",
      "address": "123 Main St"
    }
    ```

3. Add a Room to the House:
    - Once the house is created, you can add rooms to it:
    ```json
    POST /api/rooms
    {
      "name": "Living Room",
      "houseId": 1
    }
    ```

4. Add a Device to the Room:
    - Add devices to the created room, such as a thermostat or light:
    ```json
    POST /api/devices
    {
      "name": "Smart Light",
      "roomId": 1,
      "type": "Light"
    }
    ```

5. Set Automation Rules:
    - Configure automation rules for the newly added device:
    ```json
    POST /api/automationrules
    {
      "name": "Turn on light when motion detected",
      "deviceId": 1,
      "trigger": "motion",
      "action": "turn on"
    }
    ```

6. View Alerts:
    - The system will automatically generate alerts when devices encounter issues, which can be viewed in the Admin dashboard.

---

## Author
- GitHub: [github](https://github.com/Abed1313)
- LinkedIn: [linkedin](https://www.linkedin.com/in/abed-al-rahman-radwan?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=android_app)

Feel free to reach out if you have any questions or suggestions!