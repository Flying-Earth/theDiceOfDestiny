-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: localhost    Database: mygamedb
-- ------------------------------------------------------
-- Server version	8.0.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `savegame`
--

DROP TABLE IF EXISTS `savegame`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `savegame` (
  `idSaveGame` int(11) NOT NULL AUTO_INCREMENT,
  `SaveGameName` varchar(45) NOT NULL,
  `UserName` varchar(45) NOT NULL,
  `Health` int(11) NOT NULL,
  `Attack` int(11) NOT NULL,
  `Armor` int(11) NOT NULL,
  `Charge` int(11) NOT NULL,
  `MaxHealth` int(11) NOT NULL,
  `FullCharge` int(11) NOT NULL,
  `CardNum` int(11) NOT NULL,
  `TransitionA` tinyint(4) NOT NULL,
  `TransitionB` tinyint(4) NOT NULL,
  PRIMARY KEY (`idSaveGame`),
  UNIQUE KEY `idSaveGame_UNIQUE` (`idSaveGame`),
  UNIQUE KEY `SaveGameName_UNIQUE` (`SaveGameName`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `savegame`
--

LOCK TABLES `savegame` WRITE;
/*!40000 ALTER TABLE `savegame` DISABLE KEYS */;
INSERT INTO `savegame` VALUES (1,'visitorSave','visitor',385,23,15,0,400,8,3,0,0),(2,'qweSave','qwe',400,20,15,0,400,8,3,0,0),(3,'UnNetSave','UnNet',400,20,15,0,400,8,0,0,0),(4,'dongwenliSave','dongwenli',300,20,15,0,400,8,4,0,0),(5,'panSave','pan',400,23,15,0,400,8,3,0,0);
/*!40000 ALTER TABLE `savegame` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-09-17 22:04:55
