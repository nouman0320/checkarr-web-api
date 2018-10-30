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
INSERT INTO `user_log` VALUES (33,'Muhammad Shahnawaz','mshahnawaz.nuces@gmail.com','M','2018-08-16 17:13:53','F','F',NULL,NULL,NULL,NULL),(34,'Muhammad Ahmed','ahmed.nuces@gmail.com','M','2018-08-16 17:40:58','F','F',NULL,NULL,'??/w^\'??U?.$L??]?gQ????\r?J?~?#?3??3-??????????e\"s<?5?b??(','?????\Z?BW??mjV7z??Sr???	????????\\??a??}???i????~??;???&fsq?\n?F?????????Ed??0?????D??$ niG?????`?Kaa?;@.2^??W?(?u??W?U'),(35,'Muhammad shaf','shaf.nuces@gmail.com','M','2018-08-16 18:36:26','F','F',NULL,NULL,'%?J???2??a??????(}#?z??r???l???Td?\\???6?z???	|????/?Y?????\Z??','?p}????H?AvZ?f??}t???J??i??????F?[?Z????? ?????R????S??????m????mU??BkF_c0?;???kj??\\^*c??\0????9???R?^0?Q\03?tz??'),(36,'Muhammad Shah ','mshah.nuces@gmail.com','M','2018-08-30 08:28:10','F','F',NULL,NULL,'\ƒ˛˘±cQ©€ø¯Ò≠\‚KKA\” ˛≥\ÏF!)!A\ÿ\Ì\‚8#≤7æ\√¶ë+#\–:\ﬁr\Z\‹\nµ°Søˆ¶:A§ä±','\≈«áB-K˛À£!H YuGåDvZí{\‘r\ÏµrA≥º≤(cóå|2Ü\nCƒ∂∑3\⁄ÇC≤\„\Z\€QH\»4#è\ÃZÜ\Ã¸Gˇ\—ª.zz7~¨É, é|-y?ò	s4ëYéß–ì\È8öÆ°Æp\‘u\‰OYp\Î{”µú~¶`3Ez≈ü\Á‹ΩYª˝'),(37,'AliKhan','mali.nuces@gmail.com','M','2018-09-03 12:22:41','F','F',NULL,NULL,'ë:ä\‚O\ÿRf\’G\√\ﬂfÆ%\Ÿ\'CÛ\◊3\›\∆_hoX˚\ƒ\'\ZR≥\’ÿ∏°dÚ7\‚·°øc}ãîd!≥\Ì\’¿¿','ºùû\≈wQ`p\„\‡Öûè©yéìˆ^)\nz\⁄8Ö˜\ÈŸõüò∑ÿö˚E\Œ√§v\r%ø|\◊püûïøô\◊≠+\Ão~uzr\r\Î\◊\ﬁ\Ô/@ÖßùJ\œ`÷èH~®\Óyî\◊{ùH\ÕnPŸÜPv\»B÷â®´˙]èµóq∞=_9\“\Ìw\◊\”\…F\…r'),(38,'MayaKhan','maya.nuces@gmail.com','M','2018-09-03 12:29:00','F','F',NULL,NULL,'~\Î˘\‡¸NÆ\ÀH\Î-Ç\≈zëö\≈]\‡4\\±\ﬁLn,V\\\ËY|[dgtzñ≥¢\·\ÊS¸≠°\ÁØ˛\Árq\›+vˇˆ>¿','U\“lP\ﬂ»±CÒ\¬˙˛∑\\Ò#.>peB˝®ó<nop\"Æ]âß\„U\ \Ì¿±\À)ßaÒ*\‡ôÅl\Àm˘\«N3˝X\Z(ß\√<π§Ñ\‰\◊{v{\È¥Cm\Ë¨@~?ˆÛir»∑∫ÃíNJ5	:\◊!øï’ö\‚kxœü¥\”}Ø+Y•}π'),(39,'Karim Khan','karim.nuces@gmail.com','M','2018-09-08 10:09:52','F','F',NULL,NULL,'éΩ\Áó6\‹\Ì˜\Ê±9C\"≥h®˚P∑Ó™ØµZ˘\œúeOâ†µ*ru¨\ËÃááëQÄfÅÙkk\‰k#XØ','\Z¸˝∫@Yı\—\ÌÿªWYd\\£ßû®s∑?;tx,|Å êø©\≈\€\“|%P+N∫\¬◊ü\"<wp9˚~\‰ã˚±GÜü\”2ü&|gJê.XTUEn\0ò`(\‹∫Aô<±\ÂùW¨Q±™\∆M>‰âút_âÑˇ\€[ªÒlî\Ze\…ÀôÄÅ≤'),(40,'Karim uncle','karimuncle.nuces@gmail.com','M','2018-09-17 12:44:52','F','F',NULL,NULL,'®Ré\œ\∆ [c£\Î\€\∆´åJ´˘øMõÕéÒ\ZØ+\Î1{\∆\‚t-œÜÉ\ÈßFäô;æO\”ëRs∑<õ#Âö•<$f','≠	¡v\›*˙.+ÜíÛY\ÂX!$≠˝\\‹É[7+ös3\„çSù¶\€\Ô\Â\Ë’ö≥\’k\'K7mÿëbcÆ∫N\€N¿<@DA≥¶+o≥	|yC>ØØ\ fˇ6\œ\ÔR-=ï\·L^õv∑\'3\Z∞!\≈S3ñ?1^\“UW\rYπëù'),(41,'Zain Shahid','zainshahid.nuces@gmail.com','M','2018-09-17 12:49:06','F','F',NULL,NULL,'=ƒÇ≤\Â“ßJ®ñ8Ú4¡=^:}∑q‘åÖ8<\n[+5¸öE\€\ÿ.˙âÀò®ã7Ÿ©¯è¢\⁄^À§\rjl∫¡W\€','N{JX˜\È\"\Í4WﬁéE¸\ /e\›bL£ï7 \ÍE\€€Å8è\‰Wö\Ô(\·C\€+h\‰\Õ[\«26ùdÒñ;>;Ω\≈1\rWP\–!\¬\„Ùª\Ì∫0ëI\Á6Ép\»˘\n†\Â}Bñ\≈ydZ\Ê]\Ê†\ÿ.@ªà˝Å4§¶\\|≥ö\Œ\÷Ch\À3ÉòYJ¥\Â!['),(42,'Nouman Arshad','noumanshahid.nuces@gmail.com','M','2018-09-17 13:26:02','F','F',NULL,NULL,'\‡ó˙y\’mŒçV?j¡q“•Ùä¿S°|XC\” ø\Âò<!F&\ﬁ˛0AΩKXOå˚\ Y\‚\‘Zì\–˛\r1ë\Ã¡\Ó','27xMˆ\÷%o\'\ÓWl\Á;\»u◊áT\Áë•\"\Ï¶\‹Cí¶no}\Î}\0»ísÒ[±\Ÿk\–(˘ı\'\œ\—\ÕKg\ƒM√óxzº\Z£yÅÆ°•i\Ë	\Ãsf∫aô\Î¬â=∫ÖZT∂[¨ã∂Ü=\‹6˜s&hK\⁄Mj8èó\Ã@E_p'),(43,'Nouman Arshad2','noumanshahid2.nuces@gmail.com','M','2018-09-17 13:39:53','F','F',NULL,NULL,'v|\…r\ÿ„≤∑\\Bá˙T\\k*æ≠\€\Ã\⁄\√Ù–®ß\÷_\«\\°\rÒ\„}(2˛uTîqù”ã%˛°\Ã\r∏a\Œ˘','Ø/FP\‰h$l>;\‰˛\‚M]k,:0\ Ûñ!2\Õ\ BÎµ≠-¨òùNõq\ﬁˇäuspp¨\Ìù\'À∞#\\L≤•zI\ƒ)&\0∞L\Ã&ï\Õ-|\Ê\\\Ÿu1O™ˆ\“\·∆®lπì\'ÅTX∞~\Èùx⁄´\ËM\Ó\ÿ¬ä®∏\‘s\»foE±'),(44,'saad ahmed2','sadd444.nuces@gmail.com','M','2018-09-17 18:33:11','F','F',NULL,NULL,'ÄmjÛ1Ü7:Fj\Ê\‘À§cwgX.˝æ≠¬•\Ÿ[»∫∂´{éπ\Ÿ`ò®§<w¢é7\‰?å±v\¬\"hÓêÑˆ;\„dí','|\–\Â\◊\”\Œpü4\È_ı!yXÛïG!•d\Ô\Ì\œl˛!´§ç†Seπ(\‚\Á\„[\œ#JΩ\ÂR¢zÅ§\Á\'ø\—\"\Ê“†∞\⁄I\—\Íx£\ﬁO]\œz¶\\≈Ö_\ÍÄÄÿ©-\÷;\n®Gî_Ab√ê\‰_\ﬁ\·ß	86”Çk\…\ËØ;9=é$\'oO\¬');
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
