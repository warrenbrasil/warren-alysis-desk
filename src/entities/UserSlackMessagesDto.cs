using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace warren_analysis_desk;

public class UserSlackMessagesDto
{
    [Key]
    [JsonIgnore]
    public int? Id { get; set; }
    [ForeignKey("slack_messages")]
    [JsonIgnore]
    public int? SlackMessagesId { get; set; }
    public string SlackUserName { get; set; }
    [JsonIgnore]
    public SlackMessages? SlackMessages { get; set; }
}