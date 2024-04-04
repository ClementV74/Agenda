using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Text;

namespace calendrier2
{
    public class MqttClientManager
    {
        private readonly MqttClient mqttClient;
        private readonly string mqttBrokerAddress;
        private readonly string mqttUsername;
        private readonly string mqttPassword;

        public MqttClientManager(string brokerAddress, string username, string password)
        {
            mqttBrokerAddress = brokerAddress;
            mqttUsername = username;
            mqttPassword = password;
            mqttClient = new MqttClient(mqttBrokerAddress);
            mqttClient.MqttMsgPublished += MqttMsgPublished;
            mqttClient.MqttMsgPublishReceived += MqttMsgReceived;
            Connect();
        }

        private void Connect()
        {
            mqttClient.Connect(Guid.NewGuid().ToString(), mqttUsername, mqttPassword);
        }

        private void MqttMsgReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // Ajoutez ici le code pour traiter les messages MQTT reçus
        }

        private void MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            // Ajoutez ici le code pour traiter les messages MQTT publiés
        }

        public void PublishMessage(string topic, string message)
        {
            mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }

        public void Disconnect()
        {
            mqttClient.Disconnect();
        }
    }
}
