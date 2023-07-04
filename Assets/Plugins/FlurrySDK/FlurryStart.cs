using System.Collections.Generic;
using FlurrySDK;
using UnityEngine;

public class FlurryStart : MonoBehaviour
{
#if UNITY_ANDROID
    private readonly string FLURRY_API_KEY = "XV3M9WWBBMVTGQDDRQWF";
#elif UNITY_IPHONE
    private readonly string FLURRY_API_KEY = "FLURRY_IOS_API_KEY";
#else
    private readonly string FLURRY_API_KEY = null;
#endif

    private void Start()
    {
// Note: When enabling Messaging, Flurry Android should be initialized by using // Initialize Flurry once.
        new Flurry.Builder()
            .WithCrashReporting(true)
            .WithLogEnabled(true)
            .WithLogLevel(Flurry.LogLevel.DEBUG)
            .WithReportLocation(true)
            .Build(FLURRY_API_KEY);

        Debug.Log("AgentVersion: " + Flurry.GetAgentVersion());
        Debug.Log("ReleaseVersion: " + Flurry.GetReleaseVersion());
// Set Flurry preferences.
        Flurry.SetLogEnabled(true);
        Flurry.SetLogLevel(Flurry.LogLevel.VERBOSE);
// Set user preferences.
        Flurry.SetAge(36);
        Flurry.SetGender(Flurry.Gender.Female);
        Flurry.SetReportLocation(true);
// Set user properties.
        Flurry.UserProperties.Set(Flurry.UserProperties.PROPERTY_REGISTERED_USER, "True");
// Log Flurry events.
        Flurry.EventRecordStatus status = Flurry.LogEvent("Unity Event");
        Debug.Log("Log Unity Event status: " + status);
// Log Flurry timed events with parameters.
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("Author", "Flurry");
        parameters.Add("Status", "Registered");
        status = Flurry.LogEvent("Unity Event Params Timed", parameters, true);
        Debug.Log("Log Unity Event with parameters timed status: " + status);
        Flurry.EndTimedEvent("Unity Event Params Timed");
// Log Flurry standard events.
        status = Flurry.LogEvent(Flurry.Event.APP_ACTIVATED);
        Debug.Log("Log Unity Standard Event status: " + status);
        Flurry.EventParams stdParams = new Flurry.EventParams()
            .PutDouble(Flurry.EventParam.TOTAL_AMOUNT, 34.99)
            .PutBoolean(Flurry.EventParam.SUCCESS, true)
            .PutString(Flurry.EventParam.ITEM_NAME, "book 1")
            .PutString("note", "This is an awesome book to purchase !!!");
        status = Flurry.LogEvent(Flurry.Event.PURCHASED, stdParams);
        Debug.Log("Log Unity Standard Event with parameters status: " + status);
    }
}