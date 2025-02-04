using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace warren_analysis_desk;

[Table("slack_messages")]
public class SlackMessages
{
    [Key]
    public int? Id { get; set; }
    [ForeignKey("News")]
    public int IdNews { get; set; }
    public bool Marked { get; set; }
    public string? MessageId { get; set; }
    public string? BlockIds { get; set; }
    [JsonIgnore]
    public News? News { get; set; }
}
