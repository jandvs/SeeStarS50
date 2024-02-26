using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeeStarS50Lib.Models
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(CmdData))]
    [JsonSerializable(typeof(CmdDataWithRaDec))]
    [JsonSerializable(typeof(CmdDataResultRaDec))]
    [JsonSerializable(typeof(JsonParams))]
    [JsonSerializable(typeof(EventResponse))]
    [JsonSerializable(typeof(Target))]
    public partial class SourceGenerationContext : JsonSerializerContext
    {
    }

    public class EventResponse
    {
        public string Event { get; set; }
        public double TimeStamp { get; set; }
        public string state { get; set; }
    }

    public class CmdData
    {
        public CmdData()
        {
            id = "";
            method = "";
        }

        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("params")]
        public JsonParams? parameters { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //public string result { get; set; }
        public new CmdDataResultRaDec? result { get; set; }
    }

    public class CmdDataWithRaDec : CmdData
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public new CmdDataResultRaDec? result { get; set; }
    }

    public class CmdDataResultRaDec
    {
        public double ra { get; set; }
        public double dec { get; set; }
    }


    public class JsonParams
    {
        public JsonParams()
        {
            mode = "star";
            targetRaDec = new double[] { -1, -1 };
            lpFilter = 0;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("mode")]
        public string mode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("target_ra_dec")]
        public double[] targetRaDec { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("target_name")]
        public string targetName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("lp_filter")]
        public byte? lpFilter { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("restart")]
        public bool? restart { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("stage")]
        public string stage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("speed")]
        public double? speed { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("angle")]
        public double? angle { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dur_sec")]
        public int? dur_sec { get; set; }
    }

    public class Target
    {
        public Target()
        {
            Name = "";
        }
        public Target(string Name, double RA, double Dec, byte LPFilter, double SessionTime, int nRA, int nDec, double mRA, double mDec)
        {
            this.Name = Name;
            this.RA = RA;
            this.Dec = Dec;
            this.LPFilter = LPFilter;
            this.SessionTime = SessionTime;
            this.nRA = nRA;
            this.nDec = nDec;
            this.mRA = mRA;
            this.mDec = mDec;
        }
        public string Name { get; set; }
        public double RA { get; set; }
        public double Dec { get; set; }
        public byte LPFilter { get; set; }
        public double SessionTime { get; set; }
        public int nRA { get; set; }
        public int nDec { get; set; }
        public double mRA { get; set; }
        public double mDec { get; set; }

        public int SubExposure { get; set; }
    }
}
