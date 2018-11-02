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
-- Table structure for table `fan`
--

DROP TABLE IF EXISTS `fan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `fan` (
  `idFan` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) NOT NULL,
  `TimeAdded` datetime DEFAULT NULL,
  PRIMARY KEY (`idFan`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fan`
--

LOCK TABLES `fan` WRITE;
/*!40000 ALTER TABLE `fan` DISABLE KEYS */;
INSERT INTO `fan` VALUES (33,36,'2018-08-30 11:53:22'),(34,36,NULL),(35,36,'2018-08-30 12:15:56'),(39,41,'2018-09-17 19:47:46');
/*!40000 ALTER TABLE `fan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fashion`
--

DROP TABLE IF EXISTS `fashion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `fashion` (
  `idFashion` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(45) DEFAULT NULL,
  `Price` double DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Year` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idFashion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fashion`
--

LOCK TABLES `fashion` WRITE;
/*!40000 ALTER TABLE `fashion` DISABLE KEYS */;
/*!40000 ALTER TABLE `fashion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movie`
--

DROP TABLE IF EXISTS `movie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `movie` (
  `idMovie` int(11) NOT NULL AUTO_INCREMENT,
  `Rating` float DEFAULT NULL,
  `Ups` int(11) DEFAULT NULL,
  `ReleaseDate` datetime DEFAULT NULL,
  `Genre` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idMovie`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movie`
--

LOCK TABLES `movie` WRITE;
/*!40000 ALTER TABLE `movie` DISABLE KEYS */;
/*!40000 ALTER TABLE `movie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `post` (
  `POSTID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `UpVote` int(11) DEFAULT NULL,
  `Type` varchar(45) DEFAULT NULL,
  `FKType` int(11) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `Views` int(11) DEFAULT NULL,
  `Sugcount` int(11) DEFAULT NULL,
  PRIMARY KEY (`POSTID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post`
--

LOCK TABLES `post` WRITE;
/*!40000 ALTER TABLE `post` DISABLE KEYS */;
/*!40000 ALTER TABLE `post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `song`
--

DROP TABLE IF EXISTS `song`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `song` (
  `idSong` int(11) NOT NULL AUTO_INCREMENT,
  `SongName` varchar(45) NOT NULL,
  `SongAdded` datetime DEFAULT NULL,
  `Genre` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idSong`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `song`
--

LOCK TABLES `song` WRITE;
/*!40000 ALTER TABLE `song` DISABLE KEYS */;
/*!40000 ALTER TABLE `song` ENABLE KEYS */;
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
  `Activated` char(1) DEFAULT 'F',
  `Disabled` char(1) DEFAULT 'F',
  `city` varchar(45) DEFAULT NULL,
  `country` varchar(45) DEFAULT NULL,
  `password_hash` varbinary(248) DEFAULT NULL,
  `password_salt` varbinary(248) DEFAULT NULL,
  PRIMARY KEY (`iduser_log`),
  UNIQUE KEY `user_emaill_UNIQUE` (`user_emaill`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_log`
--

LOCK TABLES `user_log` WRITE;
/*!40000 ALTER TABLE `user_log` DISABLE KEYS */;
INSERT INTO `user_log` VALUES (33,'Muhammad Shahnawaz','mshahnawaz.nuces@gmail.com','M','2018-08-16 17:13:53','F','F',NULL,NULL,NULL,NULL),(34,'Muhammad Ahmed','ahmed.nuces@gmail.com','M','2018-08-16 17:40:58','F','F',NULL,NULL,'??/w^\'??U?.$L??]?gQ????\r?J?~?#?3??3-??????????e\"s<?5?b??(','?????\Z?BW??mjV7z??Sr???	????????\\??a??}???i????~??;???&fsq?\n?F?????????Ed??0?????D??$ niG?????`?Kaa?;@.2^??W?(?u??W?U'),(35,'Muhammad shaf','shaf.nuces@gmail.com','M','2018-08-16 18:36:26','F','F',NULL,NULL,'%?J???2??a??????(}#?z??r???l???Td?\\???6?z???	|????/?Y?????\Z??','?p}????H?AvZ?f??}t???J??i??????F?[?Z????? ?????R????S??????m????mU??BkF_c0?;???kj??\\^*c??\0????9???R?^0?Q\03?tz??'),(36,'Muhammad Shah ','mshah.nuces@gmail.com','M','2018-08-30 08:28:10','F','F',NULL,NULL,'\����cQ�ۿ��\�KKA\� ��\�F!)!A\�\�\�8#�7�\���+#\�:\�r\Z\�\n��S���:A���','\�ǇB-K�ˣ!H YuG�DvZ�{\�r\�rA���(c��|2�\nCĶ�3\��C�\�\Z\�QH\�4#�\�Z�\��G�\��.zz7~��,ʎ|-y?�	s4�Y��Г\�8����p\�u\�OYp\�{ӵ�~�`3Ezş\�ܽY��'),(37,'AliKhan','mali.nuces@gmail.com','M','2018-09-03 12:22:41','F','F',NULL,NULL,'�:�\�O\�R�f\�G\�\�f�%\�\'C�\�3\�\�_hoX�\�\'\ZR�\�ظ�d�7\�᡿c}��d!�\�\���','���\�wQ`p\�\�����y���^)\nz\�8��\�ٛ���ؚ�E\�äv\r%�|\�p�����\��+\�o~uzr\r\�\�\�\�/@���J\�`֏H~�\�y�\�{�H\�nPنPv\�B։����]���q�=_9\�\�w\�\�\�F\�r'),(38,'MayaKhan','maya.nuces@gmail.com','M','2018-09-03 12:29:00','F','F',NULL,NULL,'~\��\��N�\�H\�-�\�z��\�]\�4\\�\�Ln,V\\\�Y|[dgtz���\�\�S���\��\�rq\�+v��>�','U\�lP\�ȱC�\����\\�#.>peB���<nop\"�]��\�U\�\���\�)�a�*\���l\�m�\�N3�X\Z(�\�<���\�\�{v{\�Cm\�@~?��irȷ�̒NJ5	:\�!��՚\�kxϟ�\�}�+Y�}�'),(39,'Karim Khan','karim.nuces@gmail.com','M','2018-09-08 10:09:52','F','F',NULL,NULL,'��\�6\�\��\�9C\"�h��P��Z�\��eO���*ru�\�̇��Q�f��k�k\�k#X�','\Z���@Y�\�\�ػWYd\\����s�?;tx,|�ʐ��\�\�\�|%P+N�\�ן\"<wp9�~\����G��\�2�&|gJ�.�XTUEn\0�`(\��A�<�\�W�Q��\�M>䉜t_���\�[��l�\Ze\�˙���'),(40,'Karim uncle','karimuncle.nuces@gmail.com','M','2018-09-17 12:44:52','F','F',NULL,NULL,'�R�\�\� [c�\�\�\���J���M�͎�\Z�+\�1{\�\�t-φ�\�F��;�O\��Rs�<�#嚥<$f','�	�v\�*�.+���Y\�X!$��\\܃[7+�s3\��S��\�\�\�\�՚�\�k\'K7mؑbc��N\�N�<@DA��+o�	|yC>��\�f�6\�\�R-=�\�L^�v�\'3\Z�!\�S3�?1�^\�UW\rY���'),(41,'Zain Shahid','zainshahid.nuces@gmail.com','M','2018-09-17 12:49:06','F','F',NULL,NULL,'=Ă�\��ҧJ��8�4�=^:}�qԌ�8<\n[+5��E\�\�.��˘��7٩���\�^ˤ\rjl��W\�','N{JX�\�\"\�4WގE�\�/e\�bL��7 \�E\�ہ8�\�W�\�(\�C\�+h\�\�[\�26�d�;>;�\�1\rWP\�!\�\����\�0�I\�6�p\��\n�\�}B�\�ydZ\�]\�\�.@����4��\\|��\�\�Ch\��3��YJ�\�!['),(42,'Nouman Arshad','noumanshahid.nuces@gmail.com','M','2018-09-17 13:26:02','F','F',NULL,NULL,'\���y\�m΍V?j�qҥ���S�|XC\� �\�<!F&\��0A�KXO��\�Y\�\�Z�\��\r1�\��\�','27xM�\�%o\'\�Wl\�;\�uׇT\���\"\�\�C��no}\�}\0Ȓs�[�\�k\�(��\'\�\�\�Kg\�M×xz�\Z�y����i\�	\�sf�a�\�=��ZT�[����=\�6�s&hK\�Mj8��\�@E_p'),(43,'Nouman Arshad2','noumanshahid2.nuces@gmail.com','M','2018-09-17 13:39:53','F','F',NULL,NULL,'v|\�r\�㲷\\�B��T\\k*��\�\�\�\��Ш�\�_\�\\�\r�\�}(2�uT�q�Ӌ%��\�\r�a\��','�/FP\�h$l>;\��\�M]k,:0\��!2\�\�B뵭-���N�q\���uspp�\�\'˰#\\L��zI\�)&\0�L\�&�\�-|\�\\\�u1O��\�\��ƨl��\'�TX�~\�xګ\�M\�\���\�s\�foE�'),(44,'saad ahmed2','sadd444.nuces@gmail.com','M','2018-09-17 18:33:11','F','F',NULL,NULL,'�mj�1�7:Fj\�\�ˤcwgX.���¥\�[Ⱥ��{��\�`���<w��7\�?��v\�\"h�;\�d�','|\�\�\�\�\�p�4\�_�!yX�G!�d\�\�\�l�!����Se�(\�\�\�[\�#J�\�R�z��\�\'�\�\"\�Ҡ�\�I\�\�x�\�O]\�z�\\Ņ_\���ة-\�;\n�G�_AbÐ\�_\�\�	86ӂk\�\��;9=�$\'oO\�');
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

-- Dump completed on 2018-10-31  3:24:10
