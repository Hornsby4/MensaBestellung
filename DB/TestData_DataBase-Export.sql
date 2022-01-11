-- --------------------------------------------------------
-- Host:                         htlvb-project
-- Server Version:               8.0.23 - MySQL Community Server - GPL
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.2.0.6213
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportiere Datenbank Struktur für 5ahwii_mensa3
DROP DATABASE IF EXISTS `5ahwii_mensa3`;
CREATE DATABASE IF NOT EXISTS `5ahwii_mensa3` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `5ahwii_mensa3`;

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.dish
DROP TABLE IF EXISTS `dish`;
CREATE TABLE IF NOT EXISTS `dish` (
  `dish_id` int NOT NULL,
  `description` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`dish_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table for different dishes';

-- Exportiere Daten aus Tabelle 5ahwii_mensa3.dish: ~6 rows (ungefähr)
DELETE FROM `dish`;
/*!40000 ALTER TABLE `dish` DISABLE KEYS */;
INSERT INTO `dish` (`dish_id`, `description`) VALUES
	(0, 'Obst'),
	(1, 'Putencurry mit Reis'),
	(2, 'Topfen-Reis-Auflauf mit Fruchtsauce'),
	(3, 'Zwiebelrostbraten (Rind) mit Spätzle'),
	(4, 'Rindsgulasch mit Spätzle'),
	(5, 'Marmeladepalatschinken');
/*!40000 ALTER TABLE `dish` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.menu
DROP TABLE IF EXISTS `menu`;
CREATE TABLE IF NOT EXISTS `menu` (
  `dateOfDay` date NOT NULL,
  `sideDish` int DEFAULT NULL,
  `mainDish1` int DEFAULT NULL,
  `mainDish2` int DEFAULT NULL,
  `price` double DEFAULT NULL,
  `foodExchangeOpen` datetime DEFAULT NULL,
  `mensaOpen` bit(1) NOT NULL,
  PRIMARY KEY (`dateOfDay`) USING BTREE,
  KEY `FK_menu_dish` (`sideDish`),
  KEY `FK_menu_dish_2` (`mainDish1`),
  KEY `FK_menu_dish_3` (`mainDish2`),
  CONSTRAINT `FK_menu_dish` FOREIGN KEY (`sideDish`) REFERENCES `dish` (`dish_id`),
  CONSTRAINT `FK_menu_dish_2` FOREIGN KEY (`mainDish1`) REFERENCES `dish` (`dish_id`),
  CONSTRAINT `FK_menu_dish_3` FOREIGN KEY (`mainDish2`) REFERENCES `dish` (`dish_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table for a full menu per day';

-- Exportiere Daten aus Tabelle 5ahwii_mensa3.menu: ~5 rows (ungefähr)
DELETE FROM `menu`;
/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
INSERT INTO `menu` (`dateOfDay`, `sideDish`, `mainDish1`, `mainDish2`, `price`, `foodExchangeOpen`, `mensaOpen`) VALUES
	('2021-12-13', 0, 1, 2, 5.6, '2021-12-13 13:30:00', b'1'),
	('2021-12-14', 0, 1, 3, 5.6, '2021-12-14 13:30:00', b'1'),
	('2021-12-15', 0, 2, 3, 5.6, NULL, b'0'),
	('2021-12-16', NULL, NULL, NULL, NULL, '2021-12-16 13:30:00', b'0'),
	('2021-12-17', 0, 5, NULL, 5.6, '2021-12-17 13:30:00', b'1');
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.permission
DROP TABLE IF EXISTS `permission`;
CREATE TABLE IF NOT EXISTS `permission` (
  `permission_id` int NOT NULL,
  `description` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`permission_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table showing permission level';

-- Exportiere Daten aus Tabelle 5ahwii_mensa3.permission: ~3 rows (ungefähr)
DELETE FROM `permission`;
/*!40000 ALTER TABLE `permission` DISABLE KEYS */;
INSERT INTO `permission` (`permission_id`, `description`) VALUES
	(0, 'inactive'),
	(1, 'user'),
	(2, 'admin');
/*!40000 ALTER TABLE `permission` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.user
DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `user_id` int NOT NULL,
  `firstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `lastName` varchar(50) DEFAULT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `permission_id` int NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `email` (`email`),
  KEY `FK_user_permission` (`permission_id`),
  CONSTRAINT `FK_user_permission` FOREIGN KEY (`permission_id`) REFERENCES `permission` (`permission_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table for users';

-- Exportiere Daten aus Tabelle 5ahwii_mensa3.user: ~3 rows (ungefähr)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`user_id`, `firstName`, `lastName`, `email`, `permission_id`) VALUES
	(0, 'susi', 'sorglos', 'susi.sorglos@example.com', 0),
	(1, 'max', 'mustermann', 'max.mustermann@example.com', 1),
	(2, 'rudi', 'ratlos', 'rudi.ratlos@example.com', 2);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.user_orders_menu
DROP TABLE IF EXISTS `user_orders_menu`;
CREATE TABLE IF NOT EXISTS `user_orders_menu` (
  `dateOfDay` date NOT NULL,
  `user_id` int NOT NULL,
  `foodExchange` bit(1) NOT NULL,
  PRIMARY KEY (`dateOfDay`,`user_id`) USING BTREE,
  KEY `FK_user_orders_menu_user` (`user_id`),
  KEY `FK_user_oders_menu_menu` (`dateOfDay`) USING BTREE,
  CONSTRAINT `FK_user_orders_menu_menu` FOREIGN KEY (`dateOfDay`) REFERENCES `menu` (`dateOfDay`),
  CONSTRAINT `FK_user_orders_menu_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='filling table for n-n connection menu-user';

-- Exportiere Daten aus Tabelle 5ahwii_mensa3.user_orders_menu: ~3 rows (ungefähr)
DELETE FROM `user_orders_menu`;
/*!40000 ALTER TABLE `user_orders_menu` DISABLE KEYS */;
INSERT INTO `user_orders_menu` (`dateOfDay`, `user_id`, `foodExchange`) VALUES
	('2021-12-16', 1, b'0'),
	('2021-12-17', 1, b'0'),
	('2021-12-17', 2, b'1');
/*!40000 ALTER TABLE `user_orders_menu` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
