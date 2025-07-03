using LineNotification.LineNotificationModule;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LineNotificationModule
{
    public class LineNotifyClient : ILineNotifyClient
    {
        private readonly string _channelAccessToken;
        private readonly HttpClient _httpClient;
        private const string LineApiUrl = "https://api.line.me/v2/bot/message/push";

        public LineNotifyClient(string channelAccessToken)
        {
            _channelAccessToken = channelAccessToken ?? throw new ArgumentNullException(nameof(channelAccessToken));
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_channelAccessToken}");
            Log.Information("LineNotifyClient initialized with Channel Access Token.");
        }

        public async Task<bool> SendNotificationAsync(string userId, string message)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(message))
            {
                Log.Warning("SendNotificationAsync failed: userId or message is empty. userId: {UserId}, message: {Message}", userId, message);
                return false;
            }

            var payload = new
            {
                to = userId,
                messages = new[]
                {
                    new { type = "text", text = message }
                }
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                Log.Information("Sending notification to {UserId} with message: {Message}", userId, message);
                var response = await _httpClient.PostAsync(LineApiUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Log.Error("API Error for user {UserId}: StatusCode: {StatusCode}, Details: {ErrorContent}", userId, response.StatusCode, errorContent);
                }
                else
                {
                    Log.Information("Notification sent successfully to {UserId}", userId);
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while sending notification to {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> SendBroadcastAsync(string[] userIds, string message)
        {
            if (userIds == null || userIds.Length == 0 || string.IsNullOrEmpty(message))
            {
                Log.Warning("SendBroadcastAsync failed: userIds is null/empty or message is empty. userIds: {UserIds}, message: {Message}", userIds, message);
                return false;
            }

            bool anySuccess = false;
            foreach (var userId in userIds)
            {
                var success = await SendNotificationAsync(userId, message);
                anySuccess |= success;
            }

            Log.Information("Broadcast to {UserCount} users {Result}", userIds.Length, anySuccess ? "completed with at least one success" : "failed for all users");
            return anySuccess;
        }
    }
}