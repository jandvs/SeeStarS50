using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeeStarS50Lib.Models
{


    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(JsonData))]
    [JsonSerializable(typeof(JsonDataResult))]
    [JsonSerializable(typeof(JsonParams))]
    [JsonSerializable(typeof(JsonReturn))]
    [JsonSerializable(typeof(Target))]
    public partial class SourceGenerationContext : JsonSerializerContext
    {
    }

    public class JsonReturn
    {
        public string Event { get; set; }
        public string state { get; set; }
    }
    
    public class JsonData
    {
        public JsonData()
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
        public JsonParams parameters { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonDataResult result { get; set; }
    }

    public class JsonDataResult
    {
        public decimal ra { get; set; }
        public decimal dec { get; set; }
    }

    public class JsonParams
    {
        public JsonParams()
        {
            mode = "star";
            targetRaDec = new decimal[] { -1, -1 };
            lpFilter = 0;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("mode")]
        public string mode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("target_ra_dec")]
        public Decimal[] targetRaDec { get; set; }

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
    }

    public class Target
    {
        public Target()
        {
            Name = "";
        }
        public Target(string Name, decimal RA, decimal Dec, byte LPFilter, decimal SessionTime, int nRA, int nDec, decimal mRA, decimal mDec)
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
        public decimal RA { get; set; }
        public decimal Dec { get; set; }
        public byte LPFilter { get; set; }
        public decimal SessionTime { get; set; }
        public int nRA { get; set; }
        public int nDec { get; set; }
        public decimal mRA { get; set; }
        public decimal mDec { get; set; }
    }
}
