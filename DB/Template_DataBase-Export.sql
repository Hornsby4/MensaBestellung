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

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.menu
DROP TABLE IF EXISTS `menu`;
CREATE TABLE IF NOT EXISTS `menu` (
  `date` date NOT NULL,
  `sideDish` int DEFAULT NULL,
  `mainDish1` int DEFAULT NULL,
  `mainDish2` int DEFAULT NULL,
  `mensaOpen` bit(1) NOT NULL,
  PRIMARY KEY (`date`),
  KEY `FK_menu_dish` (`sideDish`),
  KEY `FK_menu_dish_2` (`mainDish1`),
  KEY `FK_menu_dish_3` (`mainDish2`),
  CONSTRAINT `FK_menu_dish` FOREIGN KEY (`sideDish`) REFERENCES `dish` (`dish_id`),
  CONSTRAINT `FK_menu_dish_2` FOREIGN KEY (`mainDish1`) REFERENCES `dish` (`dish_id`),
  CONSTRAINT `FK_menu_dish_3` FOREIGN KEY (`mainDish2`) REFERENCES `dish` (`dish_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table for a full menu per day';

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.permission
DROP TABLE IF EXISTS `permission`;
CREATE TABLE IF NOT EXISTS `permission` (
  `permission_id` int NOT NULL,
  `description` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`permission_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table showing permission level';

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.user
DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `user_id` int NOT NULL,
  `firstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `lastName` varchar(50) DEFAULT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `permission_id` int NOT NULL,
  PRIMARY KEY (`user_id`),
  KEY `FK_user_permission` (`permission_id`),
  CONSTRAINT `FK_user_permission` FOREIGN KEY (`permission_id`) REFERENCES `permission` (`permission_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table for users';

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle 5ahwii_mensa3.user_orders_menu
DROP TABLE IF EXISTS `user_orders_menu`;
CREATE TABLE IF NOT EXISTS `user_orders_menu` (
  `date` date NOT NULL,
  `user_id` int NOT NULL,
  `foodExchange` bit(1) NOT NULL,
  PRIMARY KEY (`date`,`user_id`),
  KEY `FK_user_oders_menu_menu` (`date`),
  KEY `FK_user_orders_menu_user` (`user_id`),
  CONSTRAINT `FK_user_orders_menu_menu` FOREIGN KEY (`date`) REFERENCES `menu` (`date`),
  CONSTRAINT `FK_user_orders_menu_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='filling table for n-n connection menu-user';

-- Daten Export vom Benutzer nicht ausgewählt

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
