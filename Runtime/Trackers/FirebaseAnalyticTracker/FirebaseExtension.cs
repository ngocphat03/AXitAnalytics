#if FIREBASE_ANALYTICS
namespace AXitUnityTemplate.Analytics.Runtime.Trackers.FirebaseAnalyticTracker
{
    
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class FirebaseExtension
    {
        /// <summary>
        /// Validates the string based on Firebase Analytics naming rules and returns a boolean for validity 
        /// and an output message describing the result.
        /// </summary>
        /// <param name="str">The name to validate.</param>
        /// <param name="logMessage">The output message describing the result.</param>
        /// <returns>bool: true if valid, false if invalid.</returns>
        public static bool IsNameValid(this string str, out string logMessage)
        {
            if (string.IsNullOrEmpty(str)) { logMessage = "Name is null"; return false; }
            if (str.Length > 40) { logMessage           = "Name is too long"; return false; }
            if (!char.IsLetter(str[0])) { logMessage    = "Name does not start with a letter"; return false; }

            var reservedNames = new[] { "firebase_", "google_", "ga_" };
            if (reservedNames.Any(str.Equals)) { logMessage = "Name is reserved by Google"; return false; }
            if (!Regex.IsMatch(str, "^[a-zA-Z0-9_]+$")) { logMessage = "Name contains special characters"; return false; }

            logMessage = "Valid";
            return true;
        }
    }
}
#endif
