# AXitAnalytics

**AXitAnalytics** is a customizable analytics framework designed to seamlessly integrate with Unity projects. Its goal is to provide developers with an extensible base to track user interactions and game performance across various platforms such as Firebase and ByteBrew. By abstracting the core logic, it allows developers to switch or add providers without modifying their game code extensively.


# Installation

1. Open Unity and go to **Package Manager** via **Windows > Package Manager**.
2.  Click on the **Plus (+)** icon > **Add Package from git URL**.
3. Paste the following link in the URL field: `https://github.com/ngocphat03/AXitAnalytics.git`
4. Click **Add** to install the package into your project.


# **Required Symbols:**

1. `FIREBASE_ANALYTICS`
    - Add this symbol if you are integrating **Firebase Analytics**.
    - Follow the [Firebase Unity setup guide](https://firebase.google.com/docs/unity/setup) to install the Firebase SDK and configure it properly.
2. `VCONTAINER`
    - Use this symbol if you are utilizing the **VContainer** DI framework.
    - For installation and setup, refer to the official [VContainer documentation](https://vcontainer.hadashikick.jp/).
3. `ZENJECT`
    - Define this symbol if your project uses **Zenject** for dependency injection.
    - You can find more details and setup instructions on the [Zenject GitHub page](https://github.com/modesttree/Zenject).

**NOTE:**

For more details on adding scripting define symbols in Unity, check Unity's official

Custom Scripting Symbols [documentation](https://docs.unity3d.com/6000.0/Documentation/Manual/custom-scripting-symbols.html)


# **Setup Instructions for AXitAnalytics**

The `AnalyticServiceMonoInstaller` can be configured to work standalone or with dependency injection (DI) frameworks like **VContainer** or **Zenject**. Follow the steps below for different setups.

## **1. MonoBehaviour Setup (Standalone)**

### Steps:

1. **Create a GameObject:**
    - In the Unity Editor, go to the **Hierarchy** window.
    - Right-click and select **Create Empty** to create a new GameObject.
2. **Attach the Script:**
    - Drag the `AnalyticServiceMonoInstaller.cs` script from the **Project** window and drop it onto the GameObject.
3. **Configure Persistence (Optional):**
    - Select the GameObject in the **Hierarchy**.
    - In the **Inspector**, find the `isPersistentAcrossScenes` option:
        - **Enabled (default)**: The GameObject will persist across scene changes.
        - **Disabled**: Uncheck the box if you don't need this behavior.

## **2. Setup with VContainer (Dependency Injection)**

🚧 *Feature is coming soon.*

Support for **VContainer** is under development. Stay tuned for updates.

## **3. Setup with Zenject (Dependency Injection)**

🚧*Feature is coming soon.*

Integration with **Zenject** will be available in an upcoming release.

### **Summary:**

- **Standalone Mode:** Use `AnalyticServiceMonoInstaller` as a standard MonoBehaviour.
- **VContainer or Zenject:** Replace with appropriate DI installers to enable dependency injection.


# How to Use AXitAnalytics

Follow these steps to create and track custom events using `AXitAnalytics`, whether you're using a standalone setup or integrating with a dependency injection (DI) framework.

**1. Creating a Custom Event**
Define a class that implements the `IEvent` interface to represent your event.

```csharp
using System.Collections.Generic;
using AXitUnityTemplate.Analytics.Runtime.Interface;

public class StartGameEvent : IEvent
{
    public string                     Name => "StartGame";
    public Dictionary<string, object> Data { get; }

    public StartGameEvent(int level)
    {
        this.Data = new Dictionary<string, object>
        {
            { "level", level },
            { "scene", "1.MainScene" },
        };
    }
}
```

1. **Tracking Events**

**Standalone (Without Dependency Injection)**

If you are not using DI frameworks like Zenject or VContainer, use the Singleton pattern to access the `AnalyticsService`.

```csharp
using AXitUnityTemplate.Analytics.Runtime;

public class LevelController : MonoBehaviour
{
    private void Start()
    {
        AnalyticsService.Instance.TrackEvent(new StartGameEvent(1));
    }
}
```

**With Dependency Injection**

If your project uses DI frameworks, inject `AnalyticsService` into your classes.

```csharp
using AXitUnityTemplate.Analytics.Runtime;

public class LevelController
{
    private readonly AnalyticsService analyticsService;
		
    public LevelController(AnalyticsService analyticsService)
    {
        this.analyticsService = analyticsService;
    }

    public void TrackStartGame()
    {
        this.analyticsService.TrackEvent(new StartGameEvent(1));
    }
}
```

# Supported Platforms

AXitAnalytics supports multiple platforms, including Android, iOS, and WebGL. Below are the specific instructions for setting up analytics on each platform.

### 1. **Android and iOS**

No special configuration is required for Android and iOS platforms. Simply follow the standard integration steps, and everything should work out of the box.

### 2. **WebGL Setup**

If you plan to use **Firebase Analytics** on WebGL, you need to manually configure Firebase in the `index.html` file generated by Unity.

### **Steps to Set Up Firebase for WebGL:**

1. **Modify the** `index.html` **File:**
    
    After building your project for WebGL, locate the `index.html` file in the build output folder.
    
2. **Add the Firebase Scripts:**
    
    Paste the following code inside the `<head>` tag of the `index.html` file:
    
    ```html
    <script src="https://www.gstatic.com/firebasejs/8.10.0/firebase-app.js"></script>
    <script src="https://www.gstatic.com/firebasejs/8.10.0/firebase-analytics.js"></script>
    <script>
      const firebaseConfig = {
        apiKey: "YOUR_API_KEY",
        authDomain: "YOUR_PROJECT_ID.firebaseapp.com",
        databaseURL: "https://YOUR_PROJECT_ID.firebaseio.com",
        projectId: "YOUR_PROJECT_ID",
        storageBucket: "YOUR_PROJECT_ID.appspot.com",
        messagingSenderId: "YOUR_SENDER_ID",
        appId: "YOUR_APP_ID",
        measurementId: "YOUR_MEASUREMENT_ID"
      };
      firebase.initializeApp(firebaseConfig);
      firebase.analytics();
    </script>
    ```
    

### **Important Notes:**

- **Replace the** `firebaseConfig` **object** with your project’s configuration details.
    - You can find these details in the **Firebase Console**:
        - Go to **Project Settings** > **General** tab > Scroll to the **Your apps** section.
        - Copy the configuration for your Web app.
- Make sure the Firebase SDK versions are compatible with your Unity WebGL build.


# License

This project is licensed under the MIT License, allowing you to freely use, modify, and distribute it. See the `LICENSE` file in the repository for details.
