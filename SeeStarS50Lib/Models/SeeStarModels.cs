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
    internal partial class SourceGenerationContext : JsonSerializerContext
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
            lpFilter = false;
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
        public bool? lpFilter { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("restart")]
        public bool? restart { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("stage")]
        public string stage { get; set; }
    }
}
