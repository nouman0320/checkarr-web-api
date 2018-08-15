-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: localhost    Database: checkarr
-- ------------------------------------------------------
-- Server version	8.0.11

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
-- Table structure for table `confirmationcode`
--

DROP TABLE IF EXISTS `confirmationcode`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `confirmationcode` (
  `confirmation_code` varchar(45) NOT NULL,
  `confirmation_type` varchar(45) DEFAULT NULL,
  `used` char(1) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `GeneratedOn` datetime DEFAULT NULL,
  `ExpiryTime` datetime DEFAULT NULL,
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=103 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `confirmationcode`
--

LOCK TABLES `confirmationcode` WRITE;
/*!40000 ALTER TABLE `confirmationcode` DISABLE KEYS */;
/*!40000 ALTER TABLE `confirmationcode` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `display_picture`
--

DROP TABLE IF EXISTS `display_picture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `display_picture` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `creation_date` datetime NOT NULL,
  `active` char(1) NOT NULL,
  `public_id` varchar(45) NOT NULL,
  `url` varchar(500) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `user_id_idx` (`user_id`),
  CONSTRAINT `user_id` FOREIGN KEY (`user_id`) REFERENCES `user_log` (`iduser_log`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `display_picture`
--

LOCK TABLES `display_picture` WRITE;
/*!40000 ALTER TABLE `display_picture` DISABLE KEYS */;
/*!40000 ALTER TABLE `display_picture` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `token_gen`
--

DROP TABLE IF EXISTS `token_gen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `token_gen` (
  `idtoken` int(11) NOT NULL AUTO_INCREMENT,
  `expiry_time` datetime NOT NULL,
  `token_type` varchar(45) NOT NULL,
  `user_id` int(11) NOT NULL,
  `token_string` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`idtoken`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_gen`
--

LOCK TABLES `token_gen` WRITE;
/*!40000 ALTER TABLE `token_gen` DISABLE KEYS */;
/*!40000 ALTER TABLE `token_gen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_log`
--

DROP TABLE IF EXISTS `user_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user_log` (
  `iduser_log` int(11) NOT NULL AUTO_INCREMENT,
  `user_fullname` varchar(45) NOT NULL,
  `user_emaill` varchar(45) NOT NULL,
  `user_sex` varchar(45) NOT NULL,
  `user_reg` datetime NOT NULL,
  `user_password` varchar(45) NOT NULL,
  `Activated` char(1) DEFAULT 'F',
  `Disabled` char(1) DEFAULT 'F',
  `city` varchar(45) DEFAULT NULL,
  `country` varchar(45) DEFAULT NULL,
  `password_hash` varchar(128) DEFAULT NULL,
  `password_salt` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`iduser_log`),
  UNIQUE KEY `user_emaill_UNIQUE` (`user_emaill`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_log`
--

LOCK TABLES `user_log` WRITE;
/*!40000 ALTER TABLE `user_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-08-15 16:52:46
