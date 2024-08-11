CREATE DATABASE  IF NOT EXISTS `quiz` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `quiz`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: quiz
-- ------------------------------------------------------
-- Server version	8.0.37

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240708144020_InitialCreate','8.0.6'),('20240708144435_AddQuizAccountRelationship','8.0.6'),('20240709172909_AddCollectionUpdateModelRelationships','8.0.6'),('20240711111247_AddAnswerIsCorrect','8.0.6'),('20240715102218_UpdateCollectionModel','8.0.6'),('20240724023457_AddQuizIsSaved','8.0.6'),('20240724025912_AddQuestionIsStarred','8.0.6'),('20240801100638_AddQuestionType','8.0.6'),('20240810161835_AddAccountSessionIdAndExpiresIn','8.0.6');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ExpiresIn` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `SessionId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES (1,'user@email.com','user','root','','','','0001-01-01 00:00:00.000000',''),(2,'account@email.com','','root','','','','0001-01-01 00:00:00.000000',''),(3,'person@email.com','','root','','','','0001-01-01 00:00:00.000000',''),(4,'nom@email.com','','root','','','','0001-01-01 00:00:00.000000',''),(5,'rick@email.com','','root','','','','0001-01-01 00:00:00.000000',''),(6,'morty@email.com','','root','','','','0001-01-01 00:00:00.000000',''),(7,'you@email.com','','root','','','','0001-01-01 00:00:00.000000',''),(8,'me@email.com','','root','','','','0001-01-01 00:00:00.000000','');
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `answers`
--

DROP TABLE IF EXISTS `answers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `answers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Text` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `QuestionId` int DEFAULT NULL,
  `IsCorrect` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_Answers_QuestionId` (`QuestionId`),
  CONSTRAINT `FK_Answers_Questions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `answers`
--

LOCK TABLES `answers` WRITE;
/*!40000 ALTER TABLE `answers` DISABLE KEYS */;
INSERT INTO `answers` VALUES (2,'Yu',9,1),(9,'',10,0),(10,'',9,0),(11,'',NULL,0),(12,'',NULL,0),(13,'',NULL,0),(14,'',NULL,0),(15,'',19,0),(16,'',20,0),(17,'',19,0),(18,'',20,0),(19,'Denis Villeneuve',21,1),(20,'Eric Roth',21,0),(21,'Timothée Chalamet',21,0),(22,'Timothée Chalamet',22,1),(23,'Jason Momoa',22,1),(24,'Oscar Isaac',22,1),(25,'Stellan Skarsgård',22,1),(26,'',NULL,0),(27,'',NULL,0),(28,'',26,0),(29,'',26,0),(30,'',26,0),(31,'',26,0),(32,'',26,0),(33,'',26,0),(34,'',26,0),(35,'',NULL,0),(36,'',NULL,0),(37,'',NULL,0),(38,'',NULL,0),(39,'',NULL,0),(40,'',NULL,0),(41,'',NULL,0),(42,'',NULL,0),(43,'',NULL,0),(44,'',NULL,0),(45,'',NULL,0),(46,'',NULL,0),(47,'',NULL,0),(48,'Answer',31,0),(49,'Answer',31,0),(50,'Answer',31,0),(51,'Answer',32,0),(52,'Answer',32,0),(53,'Answer',32,0),(56,'Answer',NULL,0),(57,'Answer',NULL,0),(61,'Jennifer Aniston',37,0),(62,'Jennifer Aniston',38,0),(63,'Lisa Kudrow',38,0),(64,'Courteney Cox',38,0),(65,'David Crane',38,0),(66,'Answer',NULL,0),(67,'Answer',NULL,0),(68,'Denis Villeneuve',40,1),(69,'Frank Herbert',40,0),(70,'Jon Spaihts',40,0),(71,'Zendaya',40,0),(72,'Timothée Chalamet',41,1),(73,'Zendaya',41,1),(74,'Josh Brolin',41,1),(75,'Ryan Reynolds',41,0),(76,'Action',42,1),(77,'Comedy',42,0),(78,'439',NULL,0),(79,'942',NULL,0),(80,'392',NULL,0),(81,'234',NULL,1),(83,'234',44,0),(86,'Lisa Kudrow',37,0),(87,'Answer',NULL,0),(88,'Answer',NULL,0),(89,'Answer',NULL,0),(90,'Answer',NULL,0),(91,'Answer',47,0),(92,'Answer',47,0),(93,'Answer',47,0),(94,'David Crane',37,1),(101,'123',44,0);
/*!40000 ALTER TABLE `answers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `collectionquiz`
--

DROP TABLE IF EXISTS `collectionquiz`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `collectionquiz` (
  `CollectionsId` int NOT NULL,
  `QuizzesId` int NOT NULL,
  PRIMARY KEY (`CollectionsId`,`QuizzesId`),
  KEY `IX_CollectionQuiz_QuizzesId` (`QuizzesId`),
  CONSTRAINT `FK_CollectionQuiz_Collections_CollectionsId` FOREIGN KEY (`CollectionsId`) REFERENCES `collections` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CollectionQuiz_Quizzes_QuizzesId` FOREIGN KEY (`QuizzesId`) REFERENCES `quizzes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collectionquiz`
--

LOCK TABLES `collectionquiz` WRITE;
/*!40000 ALTER TABLE `collectionquiz` DISABLE KEYS */;
INSERT INTO `collectionquiz` VALUES (32,95),(32,127);
/*!40000 ALTER TABLE `collectionquiz` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `collections`
--

DROP TABLE IF EXISTS `collections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `collections` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AccountId` int DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `LastModified` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_Collections_AccountId` (`AccountId`),
  CONSTRAINT `FK_Collections_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collections`
--

LOCK TABLES `collections` WRITE;
/*!40000 ALTER TABLE `collections` DISABLE KEYS */;
INSERT INTO `collections` VALUES (32,'Movies','Quizzes about movies',1,'2024-07-17 11:09:22.077203','2024-07-25 21:29:54.986143');
/*!40000 ALTER TABLE `collections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Text` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `QuizId` int DEFAULT NULL,
  `IsStarred` tinyint(1) NOT NULL DEFAULT '0',
  `Type` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_Questions_QuizId` (`QuizId`),
  CONSTRAINT `FK_Questions_Quizzes_QuizId` FOREIGN KEY (`QuizId`) REFERENCES `quizzes` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (9,'What is my real name?',NULL,0,0),(10,'Why am i here?',NULL,0,0),(12,'',NULL,0,0),(13,'',NULL,0,0),(19,'',NULL,0,0),(20,'',NULL,0,0),(21,'Directed by',NULL,1,0),(22,'Cast?',NULL,1,0),(26,'',NULL,0,0),(31,'Question',NULL,0,0),(32,'Question',NULL,0,0),(35,'Question',NULL,0,0),(36,'Question',NULL,0,0),(37,'Creators?',95,0,0),(38,'Stars?',95,0,0),(40,'Director by?',127,0,0),(41,'Cast?',127,0,0),(42,'Genres?',127,0,0),(44,'Episodes?',95,0,0),(47,'Question',NULL,0,0),(48,'Question',NULL,0,0),(49,'Question',NULL,0,0),(50,'Question',NULL,0,0);
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `quizzes`
--

DROP TABLE IF EXISTS `quizzes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quizzes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `LastModified` datetime(6) NOT NULL,
  `AccountId` int DEFAULT NULL,
  `Status` int NOT NULL DEFAULT '0',
  `IsSaved` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_Quizzes_AccountId` (`AccountId`),
  CONSTRAINT `FK_Quizzes_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=132 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `quizzes`
--

LOCK TABLES `quizzes` WRITE;
/*!40000 ALTER TABLE `quizzes` DISABLE KEYS */;
INSERT INTO `quizzes` VALUES (95,'Friends','Follows the personal and professional lives of six twenty to thirty year-old friends living in the Manhattan borough of New York City','2024-07-16 21:02:27.897100','2024-08-08 21:50:09.216459',1,1,0),(127,'Dune Part Two','Paul Atreides unites with Chani and the Fremen while seeking revenge against the conspirators who destroyed his family.','2024-07-24 20:12:00.135322','2024-07-27 16:27:00.017289',1,1,1),(131,'','','2024-08-10 10:24:11.701215','2024-08-10 10:24:11.701215',1,0,0);
/*!40000 ALTER TABLE `quizzes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-11  7:23:03
