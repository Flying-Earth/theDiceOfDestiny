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
-- Table structure for table `cardssequence`
--

DROP TABLE IF EXISTS `cardssequence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `cardssequence` (
  `idCardSequence` int(11) NOT NULL AUTO_INCREMENT,
  `idSaveGame` int(11) NOT NULL,
  `CardSequence` int(11) NOT NULL,
  PRIMARY KEY (`idCardSequence`),
  KEY `fk_idsavegamename_idx` (`idSaveGame`)
) ENGINE=InnoDB AUTO_INCREMENT=126 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cardssequence`
--

LOCK TABLES `cardssequence` WRITE;
/*!40000 ALTER TABLE `cardssequence` DISABLE KEYS */;
INSERT INTO `cardssequence` VALUES (1,1,0),(2,1,21),(3,1,22),(4,1,12),(5,1,9),(6,1,15),(7,1,3),(8,1,4),(9,1,18),(10,1,7),(11,1,16),(12,1,6),(13,1,23),(14,1,24),(15,1,19),(16,1,17),(17,1,5),(18,1,25),(19,1,26),(20,1,1),(21,1,10),(22,1,11),(23,1,2),(24,1,13),(25,1,8),(26,1,20),(27,1,27),(28,1,28),(29,1,29),(30,2,7),(31,2,10),(32,2,17),(33,2,13),(34,2,2),(35,2,1),(36,2,15),(37,2,6),(38,2,23),(39,2,24),(40,2,19),(41,2,9),(42,2,5),(43,2,12),(44,2,14),(45,2,3),(46,2,8),(47,2,16),(48,2,11),(49,2,4),(50,2,20),(51,3,0),(52,3,21),(53,3,22),(54,3,15),(55,3,18),(56,3,10),(57,3,12),(58,3,11),(59,3,5),(60,3,19),(61,3,6),(62,3,23),(63,3,24),(64,3,16),(65,3,2),(66,3,3),(67,3,17),(68,3,13),(69,3,14),(70,3,4),(71,3,1),(72,3,7),(73,3,9),(74,3,8),(75,3,20),(76,4,0),(77,4,21),(78,4,22),(79,4,17),(80,4,5),(81,4,11),(82,4,18),(83,4,1),(84,4,19),(85,4,6),(86,4,23),(87,4,24),(88,4,7),(89,4,9),(90,4,13),(91,4,2),(92,4,16),(93,4,14),(94,4,3),(95,4,4),(96,4,10),(97,4,8),(98,4,12),(99,4,15),(100,4,20),(101,5,0),(102,5,21),(103,5,22),(104,5,8),(105,5,17),(106,5,19),(107,5,9),(108,5,16),(109,5,13),(110,5,18),(111,5,4),(112,5,6),(113,5,23),(114,5,24),(115,5,10),(116,5,12),(117,5,15),(118,5,7),(119,5,14),(120,5,11),(121,5,1),(122,5,3),(123,5,2),(124,5,5),(125,5,20);
/*!40000 ALTER TABLE `cardssequence` ENABLE KEYS */;
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
