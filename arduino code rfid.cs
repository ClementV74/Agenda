#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9
#define SS_PIN          10

MFRC522 mfrc522(SS_PIN, RST_PIN);

void setup() {
  Serial.begin(9600);
  SPI.begin();
  mfrc522.PCD_Init();
  delay(4);
  Serial.println(F("Lecteur RFID prêt..."));
}

void loop() {
  if (!mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  if (!mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  Serial.print(F("Carte détectée. UID taille: "));
  Serial.print(mfrc522.uid.size);
  Serial.print(F(" octets"));
  Serial.println();

  Serial.print(F("UID de la carte :"));
  String content = "";
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    content.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
    content.concat(String(mfrc522.uid.uidByte[i], HEX));
  }
  content.toUpperCase();
  Serial.println(content);

  if (content.substring(1) == "D6 22 DF 2B") { // Votre UID
    Serial.println("Carte autorisée!");
    Serial.println("ok");
  } else {
    Serial.println("Carte non autorisée!");
  }

  delay(1000);
}
