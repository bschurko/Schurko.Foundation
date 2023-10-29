// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.DateTimeExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Globalization;


#nullable enable
namespace PNI.Extensions
{
  public static class DateTimeExtensions
  {
    public static WeekNum WeekOfYear(this DateTime date) => DateTimeExtensions.CalculateWeekOfYear(date, (CultureInfo) null);

    public static WeekNum WeekOfYear(this DateTime date, CultureInfo culture) => DateTimeExtensions.CalculateWeekOfYear(date, culture);

    public static DayOfWeek FirstWeekOfYearStartsWith(this DateTime date, CultureInfo culture) => DateTimeExtensions.GetLocalisedCalendarRules(culture).FirstDayOfFirstWeek;

    public static bool Between(this DateTime date, DateTime startDate, DateTime endDate) => date >= startDate && date <= endDate;

    public static int WeeksInYear(this DateTime date) => DateTimeExtensions.CalculateWeekOfYear(date, (CultureInfo) null).Week;

    public static int WeeksInYear(this DateTime date, CultureInfo culture) => DateTimeExtensions.CalculateWeekOfYear(date, culture).Week;

    public static bool IsLeapYear(this DateTime date) => DateTimeExtensions.GetStandardCalendar().IsLeapYear(date.Year);

    private static WeekNum CalculateWeekOfYear(DateTime date, CultureInfo culture)
    {
      Calendar standardCalendar = DateTimeExtensions.GetStandardCalendar();
      CalendarRules localisedCalendarRules = DateTimeExtensions.GetLocalisedCalendarRules(culture);
      WeekNum weekOfYear = new WeekNum(0, date.Year)
      {
        Week = standardCalendar.GetWeekOfYear(date, localisedCalendarRules.WeekRule, localisedCalendarRules.FirstDayOfFirstWeek)
      };
      if (weekOfYear.Week >= 52 && date.DayOfYear < 7)
        --weekOfYear.Year;
      return weekOfYear;
    }

    private static Calendar GetStandardCalendar() => (Calendar) new GregorianCalendar()
    {
      CalendarType = GregorianCalendarTypes.USEnglish
    };

    private static CalendarRules GetLocalisedCalendarRules(CultureInfo culture)
    {
      CalendarRules localisedCalendarRules = new CalendarRules();
      culture = culture ?? CultureInfo.CurrentCulture;
      switch (culture.LCID)
      {
        case 1033:
          localisedCalendarRules.WeekRule = CalendarWeekRule.FirstFullWeek;
          localisedCalendarRules.FirstDayOfFirstWeek = DayOfWeek.Saturday;
          break;
        case 2057:
          localisedCalendarRules.WeekRule = CalendarWeekRule.FirstDay;
          localisedCalendarRules.FirstDayOfFirstWeek = DayOfWeek.Sunday;
          break;
        default:
          localisedCalendarRules.WeekRule = CalendarWeekRule.FirstFullWeek;
          localisedCalendarRules.FirstDayOfFirstWeek = DayOfWeek.Saturday;
          break;
      }
      return localisedCalendarRules;
    }
  }
}
