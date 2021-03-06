﻿using NUnit.Framework;
using System;
using Domain.Core;

namespace Domain.Tests
{
    [TestFixture]
    public class SystemTimeTests
    {
        [TestCase("2017.01.01", "2017.01.01", 0)]
        [TestCase("2017.01.01", "2017.08.02", 0)]
        [TestCase("2017.01.01", "2018.01.01", 1)]
        [TestCase("2017.01.02", "2018.01.01", 0)]
        [TestCase("2017.11.02", "2020.5.01", 2)]
        public void PassedYears_InputCorrectDates_RightOutput(string startDate, string endDate, int yearDiff)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            int actualYearDiff = SystemTime.PassedYears(start, end);

            Assert.AreEqual(yearDiff, actualYearDiff);
        }
    }
}