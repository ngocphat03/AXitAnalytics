mergeInto(LibraryManager.library, {
    SetIdUser : function(id) {
        var parsedId = UTF8ToString(id);
        firebase.analytics().setUserId(parsedId);
    },
    
    LogEventWithSingleParameter: function(eventName, parameterKey, parameterValue) {
        var parsedName = UTF8ToString(eventName);
        var parsedKey = UTF8ToString(parameterKey);
        var parsedValue = UTF8ToString(parameterValue);
        
        var bundle = {};
        bundle[parsedKey] = !isNaN(parsedValue) && parsedValue !== '' ? parseFloat(parsedValue) : parsedValue;
        firebase.analytics().logEvent(parsedName, bundle);
    },

    LogEventWithMultipleParameters: function(eventName, eventParameter) {
        var parsedName = UTF8ToString(eventName);
        var parsedParams = JSON.parse(UTF8ToString(eventParameter));
        firebase.analytics().logEvent(parsedName, parsedParams);
    },

    LogEventWithoutParameters: function (eventName) {
        var event_name = UTF8ToString(eventName);
        firebase.analytics().logEvent(event_name);
    }
});