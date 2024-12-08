using System.Text;

namespace Monno.Infra.Broker;

public static class BrokerHelper
{
    public static string GetTopicName(string eventName)
    {
        if(string.IsNullOrEmpty(eventName))
            return string.Empty;

        var topic = eventName.Replace("Event", string.Empty);

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(topic[0]));

        for (var i = 1; i < topic.Length; i++)
        {
            var @char = topic[i];

            if (char.IsUpper(@char))
            {
                sb.Append('-');
                sb.Append(char.ToLowerInvariant(@char));
            }
            else
            {
                sb.Append(@char);
            }
        }

        return sb.ToString();
    }
}