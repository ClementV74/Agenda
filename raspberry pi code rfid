import serial
import paho.mqtt.client as mqtt

mqtt_server = "192.168.1.4"
mqtt_user = "clement"
mqtt_password = "clem"
topic = "authentification"

serial_port = "/dev/ttyUSB0"

def on_connect(client, userdata, flags, rc):
    print("Connecté au broker MQTT avec le code : " + str(rc))

def on_disconnect(client, userdata, rc):
    print("Déconnexion du broker MQTT")


client = mqtt.Client()
client.username_pw_set(mqtt_user, mqtt_password)
client.on_connect = on_connect
client.on_disconnect = on_disconnect

client.connect(mqtt_server, 1883, 60)
client.loop_start()

def read_serial_and_publish():
    try:
        with serial.Serial(serial_port, 9600, timeout=1) as ser:
            while True:
                line = ser.readline().strip().decode('utf-8')
                if line:
                    if line == "on":
                        client.publish(topic, "on")
                        print("Message 'on' publié sur MQTT")
                    elif line == "off":
                        client.publish(topic, "off")
                        print("Message 'off' publié sur MQTT")
                    else:
                        print("Donnée reçue depuis Arduino : " + line)
    except serial.SerialException as e:
        print("Erreur de communication série :", e)

read_serial_and_publish()
