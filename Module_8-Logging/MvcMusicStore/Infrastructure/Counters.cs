using System.Diagnostics;
using PerformanceCounterHelper;

// There is also PerformanceCounterHelper.Install.exe util that can be used to register and unregister
// the category defined by PerformanceCounterHelper.
namespace MvcMusicStore.Infrastructure
{
    // Using PerformanceCounterHelper library to create a counter category with three counters.
    [PerformanceCounterCategory("Mvc Music Store Counters", PerformanceCounterCategoryType.SingleInstance, "")]
    public enum Counters
    {
        [PerformanceCounter("Enter Home Page", "", PerformanceCounterType.NumberOfItems32)]
        GoToHome,

        [PerformanceCounter("Log into Mvc", "", PerformanceCounterType.NumberOfItems32)]
        LogIn,

        [PerformanceCounter("Log out of Mvc", "", PerformanceCounterType.NumberOfItems32)]
        LogOff
    }

    // Allows to manipulate the counters of a selected category.
    public static class CounterInstance
    {
        public static readonly CounterHelper<Counters> Counters;

        static CounterInstance()
        {
            Counters = PerformanceHelper.CreateCounterHelper<Counters>();
        }
    }
}