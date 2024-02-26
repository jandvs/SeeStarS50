using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeStarS50Lib.Libraries
{
    public static class FrontendSupport
    {




        ////////////////////////////////////////////////////////////////////////////////
        //  RA/DEC/Time conversion methods
        ////////////////////////////////////////////////////////////////////////////////
        #region RA/DEC/Time conversions
        public static string convertRaFromDouble(double ra)
        {
            int raHours = (int)ra;
            int raMinutes = (int)((ra - raHours) * 60.0);
            double raSeconds = ((ra - raHours) * 60.0 - raMinutes) * 60.0;
            return $"{raHours:00}h {raMinutes:00}m {raSeconds:00.0000}s";
        }

        public static string convertDecFromDouble(double dec)
        {
            int decSign = Math.Sign(dec);
            dec = Math.Abs(dec);
            int decDegrees = (int)dec;
            int decMinutes = (int)((dec - decDegrees) * 60.0);
            double decSeconds = ((dec - decDegrees) * 60.0 - decMinutes) * 60.0;
            return $"{(decSign > 0 ? "+" : "-")}{decDegrees:00}° {decMinutes:00}' {decSeconds:00.0000}\"";
        }

        public static double? convertRaToDouble(string ra)
        {
            // ##h ##m ##s
            string newra = ra.Replace("h", " ").Replace("m", " ").Replace("s", " ").Trim();
            while (newra.Contains("  "))
                newra = newra.Replace("  ", " ");

            string[] raParts = newra.Split(' ');
            if (raParts.Length < 3) return null;
            int raHours;
            if (!int.TryParse(raParts[0], out raHours)) return null;
            int raMinutes;
            if (!int.TryParse(raParts[1], out raMinutes)) return null;
            double raSeconds;
            if (!double.TryParse(raParts[2], out raSeconds)) return null;
            return raHours + raMinutes / 60.0 + raSeconds / 3600.0;
        }

        public static double? convertDecToDouble(string dec)
        {
            // ###° ##' ##"
            string newdec = dec.Replace("°", " ").Replace("'", " ").Replace("\"", " ").Trim();
            while (newdec.Contains("  "))
                newdec = newdec.Replace("  ", " ");

            string[] decParts = newdec.Split(' ');
            if (decParts.Length < 3) return null;
            int DecDegrees;
            if (!int.TryParse(decParts[0], out DecDegrees)) return null;
            int DecMinutes;
            if (!int.TryParse(decParts[1], out DecMinutes)) return null;
            double DecSeconds;
            if (!double.TryParse(decParts[2], out DecSeconds)) return null;
            int decSign = Math.Sign(DecDegrees);
            DecDegrees = Math.Abs(DecDegrees);
            return (DecDegrees + DecMinutes / 60.0 + DecSeconds / 3600.0) * decSign;
        }

        public static double? convertTimeToDouble(string time)
        {
            // ## Hours ## Minutes
            string newtime = time.Replace("Hours", "").Replace("Minutes", "").Trim();
            while (newtime.Contains("  "))
                newtime = newtime.Replace("  ", " ");

            string[] timeParts = newtime.Split(' ');
            if (timeParts.Length < 2) return null;
            int timeHours;
            if (!int.TryParse(timeParts[0], out timeHours)) return null;
            int timeMinutes;
            if (!int.TryParse(timeParts[1], out timeMinutes)) return null;
            return timeHours + timeMinutes / 60.0;
        }

        public static string? convertTimeFromDouble(double time)
        {
            int timeHours = (int)time;
            int timeMinutes = (int)((time - timeHours) * 60.0);
            return $"{timeHours:00} Hours {timeMinutes:00} Minutes";
        }

        /*
        // Convert equatorial coordinates to alt-az coordinates 
        public static Tuple<double, double> ConvertEquatorialToAltAz(double rightAscensionHours, double declinationDegrees, DateTime observationTime, double observerLongitudeDegrees, double observerLatitudeDegrees)
        {
            // Convert right ascension from hours to degrees
            double rightAscensionDegrees = rightAscensionHours * 15.0;

            // Calculate local sidereal time (LST) at the observer's location
            double lstHours = CalculateLocalSiderealTime(observationTime, observerLongitudeDegrees);

            // Calculate hour angle (HA) in degrees
            double hourAngleDegrees = lstHours * 15.0 - rightAscensionDegrees;

            // Calculate altitude (ALT) and azimuth (AZ) using trigonometric formulas
            double sinAltitude = Math.Sin(declinationDegrees * Math.PI / 180.0) * Math.Sin(observerLatitudeDegrees * Math.PI / 180.0) +
                                 Math.Cos(declinationDegrees * Math.PI / 180.0) * Math.Cos(observerLatitudeDegrees * Math.PI / 180.0) * Math.Cos(hourAngleDegrees * Math.PI / 180.0);
            double altitudeDegrees = Math.Asin(sinAltitude) * 180.0 / Math.PI;

            double cosAzimuth = (Math.Sin(declinationDegrees * Math.PI / 180.0) - Math.Sin(altitudeDegrees * Math.PI / 180.0) * Math.Sin(observerLatitudeDegrees * Math.PI / 180.0)) /
                                (Math.Cos(altitudeDegrees * Math.PI / 180.0) * Math.Cos(observerLatitudeDegrees * Math.PI / 180.0));
            double azimuthDegrees = Math.Acos(cosAzimuth) * 180.0 / Math.PI;

            // Convert azimuth to the correct quadrant (0° to 360°)
            if (Math.Sin(hourAngleDegrees * Math.PI / 180.0) > 0)
                azimuthDegrees = 360.0 - azimuthDegrees;

            var altaz = new Tuple<double, double>((double)altitudeDegrees, (double)azimuthDegrees);

            Console.WriteLine($"Altitude: {altitudeDegrees}°");
            Console.WriteLine($"Azimuth: {azimuthDegrees}°");

            return altaz;
        }

        // Convert alt-az coordinates to equatorial coordinates
        public static Tuple<double, double> ConvertAltAzToEquatorial(double altitudeDegrees, double azimuthDegrees, DateTime observationTime, double observerLongitudeDegrees, double observerLatitudeDegrees)
        {
            // Calculate local sidereal time (LST) at the observer's location
            double lstHours = CalculateLocalSiderealTime(observationTime, observerLongitudeDegrees);

            // Calculate hour angle (HA) in degrees
            double hourAngleDegrees = lstHours * 15.0 - azimuthDegrees;

            // Calculate declination (DEC) using trigonometric formulas
            double sinDeclination = Math.Sin(altitudeDegrees * Math.PI / 180.0) * Math.Sin(observerLatitudeDegrees * Math.PI / 180.0) +
                                    Math.Cos(altitudeDegrees * Math.PI / 180.0) * Math.Cos(observerLatitudeDegrees * Math.PI / 180.0) * Math.Cos(hourAngleDegrees * Math.PI / 180.0);
            double declinationDegrees = Math.Asin(sinDeclination) * 180.0 / Math.PI;

            // Calculate right ascension (RA) using the hour angle and declination
            double rightAscensionDegrees = lstHours * 15.0 - declinationDegrees;

            // Convert right ascension to hours
            double rightAscensionHours = rightAscensionDegrees / 15.0;

            var raDec = new Tuple<double, double>((double)rightAscensionHours, (double)declinationDegrees);

            Console.WriteLine($"Right Ascension: {rightAscensionHours} hours");
            Console.WriteLine($"Declination: {declinationDegrees}°");

            return raDec;
        }


        // Calculate local sidereal time (LST) at the observer's location
        private static double CalculateLocalSiderealTime(DateTime observationTime, double observerLongitudeDegrees)
        {
            double julianDate = observationTime.ToOADate() + 2415018.5;
            double t = (julianDate - 2451545.0) / 36525.0;
            double gmst = 280.46061837 + 360.98564736629 * (julianDate - 2451545.0) + 0.000387933 * t * t - t * t * t / 38710000.0;
            double lstHours = (gmst + observerLongitudeDegrees) / 15.0;
            return lstHours;
        }
        */

        #endregion


    }
}
