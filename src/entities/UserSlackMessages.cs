using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace warren_analysis_desk;

[Table("user_slack_messages")]
public class UserSlackMessages
{
    [Key]
    public int? Id { get; set; }
    [ForeignKey("slack_messages")]
    public int? SlackMessagesId { get; set; }
    public bool Marked { get; set; }
    public string SlackUserName { get; set; }
    public string SlackUserId { get; set; }
    [JsonIgnore]
    public SlackMessages? SlackMessages { get; set; }

    public UserSlackMessages() { }

    public UserSlackMessages(UserSlackMessagesDto dto)
    {
        Id = dto.Id;
        SlackMessagesId = dto.SlackMessagesId ?? 0;
        SlackUserName = dto.SlackUserName;
        SlackUserId = dto.SlackUserId;
        Marked = dto.Marked;
    }
}