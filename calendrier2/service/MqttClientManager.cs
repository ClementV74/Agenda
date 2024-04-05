using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Text;
using System.Threading.Tasks;

namespace calendrier2.service
{
    public class MqttClientManager
    {
        private readonly MqttClient mqttClient; // Client MQTT
        private readonly string mqttBrokerAddress; // Adresse MQTT
        private readonly string mqttUsername; // Nom d'utilisateur MQTT
        private readonly string mqttPassword; // Mot de passe MQTT

        public MqttClientManager(string brokerAddress, string username, string password)  
        {  
            mqttBrokerAddress = brokerAddress; // Définir l'adresse MQTT
            mqttUsername = username; // Définir le nom d'utilisateur MQTT
            mqttPassword = password; // Définir le mot de passe MQTT
            mqttClient = new MqttClient(mqttBrokerAddress); // Créer une instance de client MQTT
         
            Connect(); // Se connecter au broker MQTT
        }

        private async void Connect()
        {
            await Task.Delay(TimeSpan.FromSeconds(2)); // Attente de 2 secondes avant de se connecter
            mqttClient.Connect(Guid.NewGuid().ToString(), mqttUsername, mqttPassword); // Se connecter au broker MQTT
        }

      
        public void PublishMessage(string topic, string message)
        {
            mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true); // Publier un message MQTT
        }

        public void Disconnect()
        {
            mqttClient.Disconnect(); // Déconnecter le client MQTT
        }
    }
}
