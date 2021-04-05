-- MySqlBackup.NET 2.0.4
-- Dump Time: 2021-04-05 21:43:56
-- --------------------------------------
-- Server version 8.0.23 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of Bike_Model
-- 

DROP TABLE IF EXISTS `Bike_Model`;
CREATE TABLE IF NOT EXISTS `Bike_Model` (
  `idBike_Model` int NOT NULL AUTO_INCREMENT,
  `Color` varchar(45) DEFAULT NULL,
  `Size` varchar(45) DEFAULT NULL,
  `Type_Model` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idBike_Model`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Bike_Model
-- 

/*!40000 ALTER TABLE `Bike_Model` DISABLE KEYS */;
INSERT INTO `Bike_Model`(`idBike_Model`,`Color`,`Size`,`Type_Model`) VALUES
(1,'Red','26','City'),
(2,'Blue','26','City'),
(3,'Black','26','City'),
(4,'Red','28','City'),
(5,'Blue','28','City'),
(6,'Black','28','City'),
(7,'Red','26','Adventure'),
(8,'Blue','26','Adventure'),
(9,'Black','26','Adventure'),
(10,'Red','28','Adventure'),
(11,'Blue','28','Adventure'),
(12,'Black','28','Adventure'),
(13,'Red','26','Explorer'),
(14,'Blue','26','Explorer'),
(15,'Black','26','Explorer'),
(16,'Red','28','Explorer'),
(17,'Blue','28','Explorer'),
(18,'Black','28','Explorer'),
(41,'Red','26','tandem'),
(42,'Black','26','test');
/*!40000 ALTER TABLE `Bike_Model` ENABLE KEYS */;

-- 
-- Definition of Bike_Parts
-- 

DROP TABLE IF EXISTS `Bike_Parts`;
CREATE TABLE IF NOT EXISTS `Bike_Parts` (
  `Id_Bike_Parts` int NOT NULL AUTO_INCREMENT,
  `Bike_Parts_Name` varchar(45) NOT NULL,
  `Quantity` int NOT NULL,
  `Location` varchar(45) NOT NULL,
  `Price` decimal(10,0) NOT NULL,
  `Provider` varchar(45) NOT NULL,
  `Time_To_Build` varchar(45) NOT NULL,
  PRIMARY KEY (`Id_Bike_Parts`)
) ENGINE=InnoDB AUTO_INCREMENT=77 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Bike_Parts
-- 

/*!40000 ALTER TABLE `Bike_Parts` DISABLE KEYS */;
INSERT INTO `Bike_Parts`(`Id_Bike_Parts`,`Bike_Parts_Name`,`Quantity`,`Location`,`Price`,`Provider`,`Time_To_Build`) VALUES
(1,'kit de frein',35,'k1',79,'TZUSCRLS','4'),
(2,'kit vitesse',10,'k2',50,'JHDFMJJX','15'),
(3,'cadre_red_26',50,'c3',150,'QNBVSFJS','1'),
(4,'cadre_blue_26',10,'c4',150,'QNBVSFJS','1'),
(5,'cadre_black_26',10,'c5',150,'QNBVSFJS','1'),
(6,'cadre_red_28',10,'c6',150,'QNBVSFJS','1'),
(7,'cadre_blue_28',10,'c7',150,'QNBVSFJS','1'),
(8,'cadre_black_28',10,'c8',150,'QNBVSFJS','1'),
(9,'cadre renforcé_red_26',10,'c9',200,'EPROXATW','6'),
(10,'cadre renforcé_blue_26',10,'c10',200,'EPROXATW','6'),
(11,'cadre renforcé_black_26',10,'c11',200,'EPROXATW','6'),
(12,'cadre renforcé_red_28',10,'c12',200,'EPROXATW','6'),
(13,'cadre renforcé_blue_28',10,'c13',200,'EPROXATW','6'),
(14,'cadre renforcé_black_28',10,'c14',200,'EPROXATW','6'),
(15,'kit pédalier',35,'k5',12,'VAHODPHQ','7'),
(16,'cassette de pignons',14,'z6',38,'XJAHIEVP','14'),
(17,'catadioptre',18,'z7',1,'RZXJEDSF','1'),
(18,'chaîne',10,'z8',20,'WFFXKACX','11'),
(19,'garde-boue_red_26',10,'g9',8,'NFFKDBYZ','5'),
(20,'garde-boue_blue_26',10,'g10',8,'NFFKDBYZ','5'),
(21,'garde-boue_black_26',10,'g11',8,'NFFKDBYZ','5'),
(22,'garde-boue_red_28',10,'g12',8,'NFFKDBYZ','5'),
(23,'garde-boue_blue_28',10,'g13',8,'NFFKDBYZ','5'),
(24,'garde-boue_black_28',10,'g14',8,'NFFKDBYZ','5'),
(25,'garde-boue large_26',10,'g15',13,'ZHWXORAB','8'),
(26,'garde-boue large_28',10,'g16',13,'ZHWXORAB','8'),
(27,'chambre à air',10,'z11',5,'ZTMXWHCP','3'),
(28,'dérailleur',10,'d12',77,'PHDOEEUE','3'),
(29,'disque de frien',10,'d13',60,'AYDCTNBU','9'),
(30,'éclairage',14,'e14',2,'NSIZCRCI','7'),
(31,'fourche_26',10,'f15',76,'YIXYPOIO','5'),
(32,'fourche_28',14,'f16',76,'YIXYPOIO','5'),
(33,'guidon',14,'g17',37,'KZJDTXPB','14'),
(34,'plateau',14,'p17',15,'XMKHRNBQ','11'),
(35,'pneu_26',10,'p18',16,'VOXBZOHF','1'),
(36,'pneu_28',14,'p19',16,'VOXBZOHF','1'),
(37,'pneu large_26',10,'p20',22,'CNNILLQL','3'),
(38,'pneu large_28',10,'p21',22,'CNNILLQL','3'),
(39,'porte-bagage',14,'p22',38,'OBUQWIIF','10'),
(40,'roue_26',10,'r21',40,'QCPVKHRC','14'),
(41,'roue_28',14,'r22',40,'QCPVKHRC','14'),
(42,'selle',14,'s22',29,'VNNWLFYS','10'),
(52,'béquille',10,'b0',10,'TRXYXUYA','9');
/*!40000 ALTER TABLE `Bike_Parts` ENABLE KEYS */;

-- 
-- Definition of Bikes
-- 

DROP TABLE IF EXISTS `Bikes`;
CREATE TABLE IF NOT EXISTS `Bikes` (
  `Bikes_Id` int NOT NULL AUTO_INCREMENT,
  `Bike_Type` varchar(45) DEFAULT NULL,
  `Price` decimal(10,0) DEFAULT NULL,
  `Bike_total_time` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Bikes_Id`),
  KEY `Type` (`Bike_Type`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Bikes
-- 

/*!40000 ALTER TABLE `Bikes` DISABLE KEYS */;

/*!40000 ALTER TABLE `Bikes` ENABLE KEYS */;

-- 
-- Definition of Detailed_Schedules
-- 

DROP TABLE IF EXISTS `Detailed_Schedules`;
CREATE TABLE IF NOT EXISTS `Detailed_Schedules` (
  `Week_Name` varchar(50) NOT NULL,
  `Id_Order_Details` int NOT NULL,
  `Assembled_by` varchar(45) DEFAULT NULL,
  `Started` varchar(50) DEFAULT NULL,
  `Finished` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Week_Name`,`Id_Order_Details`),
  KEY `FK_schedules` (`Week_Name`),
  KEY `FK_Details_idx` (`Id_Order_Details`),
  CONSTRAINT `FK_Details` FOREIGN KEY (`Id_Order_Details`) REFERENCES `Order_Details` (`Id_Order_Details`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Detailed_Schedules
-- 

/*!40000 ALTER TABLE `Detailed_Schedules` DISABLE KEYS */;
INSERT INTO `Detailed_Schedules`(`Week_Name`,`Id_Order_Details`,`Assembled_by`,`Started`,`Finished`) VALUES
('15',2812,NULL,NULL,NULL),
('15',2813,NULL,NULL,NULL),
('15',2814,NULL,NULL,NULL),
('15',2815,NULL,NULL,NULL),
('15',2816,NULL,NULL,NULL),
('15',2820,NULL,NULL,NULL),
('15',2822,NULL,NULL,NULL),
('17',2751,NULL,NULL,NULL),
('17',2753,NULL,NULL,NULL),
('17',2755,NULL,NULL,NULL),
('17',2758,NULL,NULL,NULL),
('Week : 15',2737,'Assembler2','Sunday 15:46','Sunday 15:46'),
('Week : 15',2739,'Assembler2','Sunday 16:54','Sunday 16:54'),
('Week : 15',2741,'','',''),
('Week : 15',2743,NULL,NULL,NULL),
('Week : 15',2744,NULL,NULL,NULL),
('Week : 15',2745,NULL,NULL,NULL),
('Week : 15',2746,NULL,NULL,NULL),
('Week : 15',2747,NULL,NULL,NULL),
('Week : 15',2748,NULL,NULL,NULL),
('Week : 15',2749,NULL,NULL,NULL),
('Week : 15',2801,'','',''),
('Week : 15',2803,'Assembler1','set on active','Tuesday 23:29'),
('Week : 15',2805,'Assembler1','set on active','Sunday 11:19'),
('Week : 15',2806,'Assembler1','Sunday 11:19','Sunday 11:19'),
('Week : 15',2807,'Assembler1','Sunday 11:19','Sunday 11:19'),
('Week : 16',2756,'Manager','set on active','Sunday 22:3'),
('Week : 16',2759,'Assembler1','Sunday 16:13','Sunday 16:13'),
('Week : 16',2762,'Assembler2','Sunday 16:54','Sunday 16:54'),
('Week : 16',2764,'Assembler2','Sunday 23:45','Sunday 23:45'),
('Week : 16',2766,NULL,NULL,NULL);
/*!40000 ALTER TABLE `Detailed_Schedules` ENABLE KEYS */;

-- 
-- Definition of Order_Bikes
-- 

DROP TABLE IF EXISTS `Order_Bikes`;
CREATE TABLE IF NOT EXISTS `Order_Bikes` (
  `Id_Order` int NOT NULL AUTO_INCREMENT,
  `Customer_Name` varchar(45) NOT NULL,
  `Total_Price` decimal(10,0) NOT NULL,
  `Order_Date` varchar(45) NOT NULL,
  `Shipping_Time` varchar(45) NOT NULL,
  PRIMARY KEY (`Id_Order`),
  KEY `Primaire` (`Customer_Name`)
) ENGINE=InnoDB AUTO_INCREMENT=335 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Order_Bikes
-- 

/*!40000 ALTER TABLE `Order_Bikes` DISABLE KEYS */;
INSERT INTO `Order_Bikes`(`Id_Order`,`Customer_Name`,`Total_Price`,`Order_Date`,`Shipping_Time`) VALUES
(314,'test',34560,'27/03/2021 23:01:49','03/04/2021 00:00:00'),
(315,'testTime',110020,'27/03/2021 23:03:56','10/04/2021 00:00:00'),
(316,'testTime3',89288,'27/03/2021 23:09:06','10/04/2021 00:00:00'),
(317,'testTime4',6102,'27/03/2021 23:09:39','10/04/2021 00:00:00'),
(318,'testTime6',5760,'27/03/2021 23:10:32','10/04/2021 00:00:00'),
(319,'speedTestCart',87784,'28/03/2021 10:36:14','18/04/2021 00:00:00'),
(320,'test',69425,'28/03/2021 17:16:30','02/05/2021 00:00:00'),
(321,'test',8870,'29/03/2021 15:09:50','03/05/2021 00:00:00'),
(322,'TestApp',16159,'03/04/2021 14:59:26','08/05/2021 00:00:00'),
(323,'stock',3548,'03/04/2021 15:20:14','08/05/2021 00:00:00'),
(324,'TestStaticClass',10732,'03/04/2021 16:52:26','15/05/2021 00:00:00'),
(325,'testOrderID',10629,'03/04/2021 17:32:07','15/05/2021 00:00:00'),
(326,'TestOrderID2',1774,'03/04/2021 17:35:30','15/05/2021 00:00:00'),
(327,'testOrder',1808,'03/04/2021 17:42:34','15/05/2021 00:00:00'),
(328,'testqty',0,'04/04/2021 11:18:15','09/05/2021 00:00:00'),
(329,'test',887,'04/04/2021 11:37:55','09/05/2021 00:00:00'),
(330,'testMessageBox',1808,'04/04/2021 11:40:48','09/05/2021 00:00:00'),
(331,'csv',2712,'04/04/2021 11:59:36','09/05/2021 00:00:00'),
(332,'testClassRepresentative',2742,'04/04/2021 16:23:59','09/05/2021 00:00:00'),
(333,'RepresentativeTest',887,'05/04/2021 10:33:49','10/05/2021 0:00:00'),
(334,'Stock',2712,'05/04/2021 10:35:00','17/05/2021 0:00:00');
/*!40000 ALTER TABLE `Order_Bikes` ENABLE KEYS */;

-- 
-- Definition of Order_Detailed_Part
-- 

DROP TABLE IF EXISTS `Order_Detailed_Part`;
CREATE TABLE IF NOT EXISTS `Order_Detailed_Part` (
  `idOrder_Detailed_Part` int NOT NULL AUTO_INCREMENT,
  `Id_Order` int DEFAULT NULL,
  `Id_Bike_Parts` int NOT NULL,
  `Quantity` int DEFAULT NULL,
  `Price` int DEFAULT NULL,
  `State` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idOrder_Detailed_Part`,`Id_Bike_Parts`),
  UNIQUE KEY `idOrder_Detailed_Part_UNIQUE` (`idOrder_Detailed_Part`),
  KEY `FK_Bike_Parts_idx` (`Id_Bike_Parts`),
  CONSTRAINT `FK_Bike_Parts` FOREIGN KEY (`Id_Bike_Parts`) REFERENCES `Bike_Parts` (`Id_Bike_Parts`)
) ENGINE=InnoDB AUTO_INCREMENT=422 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Order_Detailed_Part
-- 

/*!40000 ALTER TABLE `Order_Detailed_Part` DISABLE KEYS */;
INSERT INTO `Order_Detailed_Part`(`idOrder_Detailed_Part`,`Id_Order`,`Id_Bike_Parts`,`Quantity`,`Price`,`State`) VALUES
(202,43,29,4,172,'Received'),
(203,43,30,4,980,'Received'),
(204,43,32,4,884,'Received'),
(205,43,33,4,416,'Received'),
(206,43,34,4,808,'Received'),
(207,43,36,4,676,'Received'),
(208,43,39,4,488,'Received'),
(210,43,42,4,444,'Received'),
(211,43,52,4,608,'Not Received'),
(212,44,1,0,0,'Not Received'),
(213,44,2,0,0,'Not Received'),
(214,44,12,2,190,'Not Received'),
(215,44,15,2,206,'Not Received'),
(216,44,16,0,0,'Not Received'),
(217,44,17,4,400,'Received'),
(219,44,26,2,326,'Not Received'),
(220,44,27,4,384,'Not Received'),
(221,44,28,2,22,'Not Received'),
(222,44,29,4,172,'Not Received'),
(223,44,32,2,442,'Not Received'),
(224,44,33,2,208,'Not Received'),
(225,44,34,2,404,'Not Received'),
(226,44,38,4,804,'Not Received'),
(227,44,41,4,160,'Not Received'),
(228,44,42,2,222,'Not Received'),
(229,44,52,2,304,'Not Received'),
(230,45,1,15,1500,'Not Received'),
(231,45,2,15,1605,'Not Received'),
(232,45,7,3,6,'Not Received'),
(233,45,15,15,1545,'Not Received'),
(234,45,16,11,957,'Not Received'),
(235,45,17,56,5600,'Not Received'),
(236,45,18,15,2415,'Not Received'),
(237,45,23,3,168,'Not Received'),
(238,45,27,30,2880,'Not Received'),
(239,45,28,15,165,'Not Received'),
(240,45,29,30,1290,'Not Received'),
(241,45,30,11,2695,'Not Received'),
(242,45,33,11,1144,'Not Received'),
(243,45,34,11,2222,'Not Received'),
(244,45,36,2,338,'Not Received'),
(245,45,39,11,1342,'Not Received'),
(246,45,41,2,80,'Not Received'),
(247,45,42,15,1665,'Not Received'),
(248,45,52,15,2280,'Not Received'),
(249,45,4,7,14,'Not Received'),
(250,45,20,7,392,'Not Received'),
(251,45,31,12,2652,'Not Received'),
(252,45,35,14,2366,'Not Received'),
(253,45,40,24,960,'Not Received'),
(254,45,25,5,815,'Not Received'),
(255,45,37,10,2010,'Not Received'),
(256,46,1,15,1500,'Received'),
(257,46,2,15,1605,'Not Received'),
(258,46,7,3,6,'Not Received'),
(259,46,15,15,1545,'Not Received'),
(260,46,16,11,957,'Not Received'),
(261,46,17,56,5600,'Not Received'),
(262,46,18,15,2415,'Not Received'),
(263,46,23,3,168,'Not Received'),
(264,46,27,30,2880,'Not Received'),
(265,46,28,15,165,'Not Received'),
(266,46,29,30,1290,'Not Received'),
(267,46,30,11,2695,'Not Received'),
(268,46,33,11,1144,'Not Received'),
(269,46,34,11,2222,'Not Received'),
(270,46,36,2,338,'Not Received'),
(271,46,39,11,1342,'Not Received'),
(272,46,41,2,80,'Not Received'),
(273,46,42,15,1665,'Not Received'),
(274,46,52,15,2280,'Not Received'),
(275,46,4,7,14,'Not Received'),
(276,46,20,7,392,'Not Received'),
(277,46,31,12,2652,'Not Received'),
(278,46,35,14,2366,'Not Received'),
(279,46,40,24,960,'Not Received'),
(280,46,25,5,815,'Not Received'),
(281,46,37,10,2010,'Not Received'),
(282,47,1,15,1500,'Not Received'),
(283,47,2,15,1605,'Not Received'),
(284,47,7,3,6,'Not Received'),
(285,47,15,15,1545,'Not Received'),
(286,47,16,11,957,'Not Received'),
(287,47,17,56,5600,'Not Received'),
(288,47,18,15,2415,'Not Received'),
(289,47,23,3,168,'Not Received'),
(290,47,27,30,2880,'Not Received'),
(291,47,28,15,165,'Not Received'),
(292,47,29,30,1290,'Not Received'),
(293,47,30,11,2695,'Not Received'),
(294,47,33,11,1144,'Not Received'),
(295,47,34,11,2222,'Not Received'),
(296,47,36,2,338,'Not Received'),
(297,47,39,11,1342,'Not Received'),
(298,47,41,2,80,'Not Received'),
(299,47,42,15,1665,'Not Received'),
(300,47,52,15,2280,'Not Received'),
(301,47,4,7,14,'Not Received'),
(302,47,20,7,392,'Not Received'),
(303,47,31,12,2652,'Not Received'),
(304,47,35,14,2366,'Not Received'),
(305,47,40,24,960,'Not Received'),
(306,47,25,5,815,'Not Received'),
(307,47,37,10,2010,'Not Received'),
(308,48,1,15,1500,'Not Received'),
(309,48,2,15,1605,'Not Received'),
(310,48,7,3,6,'Not Received'),
(311,48,15,15,1545,'Not Received'),
(312,48,16,11,957,'Not Received'),
(313,48,17,56,5600,'Not Received'),
(314,48,18,15,2415,'Not Received'),
(315,48,23,3,168,'Not Received'),
(316,48,27,30,2880,'Not Received'),
(317,48,28,15,165,'Not Received'),
(318,48,29,30,1290,'Not Received'),
(319,48,30,11,2695,'Not Received'),
(320,48,33,11,1144,'Not Received'),
(321,48,34,11,2222,'Not Received'),
(322,48,36,2,338,'Not Received'),
(323,48,39,11,1342,'Not Received'),
(324,48,41,2,80,'Not Received'),
(325,48,42,15,1665,'Not Received'),
(326,48,52,15,2280,'Not Received'),
(327,48,4,7,14,'Not Received'),
(328,48,20,7,392,'Not Received'),
(329,48,31,12,2652,'Not Received'),
(330,48,35,14,2366,'Not Received'),
(331,48,40,24,960,'Not Received'),
(332,48,25,5,815,'Not Received'),
(333,48,37,10,2010,'Not Received'),
(334,49,1,15,1500,'Not Received'),
(335,49,2,15,1605,'Not Received'),
(336,49,7,3,6,'Not Received'),
(337,49,15,15,1545,'Not Received'),
(338,49,16,11,957,'Not Received'),
(339,49,17,56,5600,'Not Received'),
(340,49,18,15,2415,'Not Received'),
(341,49,23,3,168,'Not Received'),
(342,49,27,30,2880,'Not Received'),
(343,49,28,15,165,'Not Received'),
(344,49,29,30,1290,'Not Received'),
(345,49,30,11,2695,'Not Received'),
(346,49,33,11,1144,'Not Received'),
(347,49,34,11,2222,'Not Received'),
(348,49,36,2,338,'Not Received'),
(349,49,39,11,1342,'Not Received'),
(350,49,41,2,80,'Not Received'),
(351,49,42,15,1665,'Not Received'),
(352,49,52,15,2280,'Not Received'),
(353,49,4,7,14,'Not Received'),
(354,49,20,7,392,'Not Received'),
(355,49,31,12,2652,'Not Received'),
(356,49,35,14,2366,'Not Received'),
(357,49,40,24,960,'Not Received'),
(358,49,25,5,815,'Not Received'),
(359,49,37,10,2010,'Not Received'),
(360,50,1,15,1185,'Not Received'),
(361,50,2,15,750,'Not Received'),
(362,50,7,3,450,'Not Received'),
(363,50,15,15,180,'Not Received'),
(364,50,16,11,418,'Not Received'),
(365,50,17,56,56,'Not Received'),
(366,50,18,15,300,'Not Received'),
(367,50,23,3,24,'Not Received'),
(368,50,27,30,150,'Not Received'),
(369,50,28,15,1155,'Not Received'),
(370,50,29,30,1800,'Not Received'),
(371,50,30,11,22,'Not Received'),
(372,50,33,11,407,'Not Received'),
(373,50,34,11,165,'Not Received'),
(374,50,36,2,32,'Not Received'),
(375,50,39,11,418,'Not Received'),
(376,50,41,2,80,'Not Received'),
(377,50,42,15,435,'Not Received'),
(378,50,52,15,150,'Not Received'),
(379,50,4,7,1050,'Not Received'),
(380,50,20,7,56,'Not Received'),
(381,50,31,12,912,'Not Received'),
(382,50,35,14,224,'Not Received'),
(383,50,40,24,960,'Not Received'),
(384,50,25,5,65,'Not Received'),
(385,50,37,10,220,'Not Received'),
(386,51,2,7,350,'Not Received'),
(387,51,13,7,1400,'Not Received'),
(388,51,15,7,84,'Not Received'),
(389,51,16,3,114,'Not Received'),
(390,51,17,24,24,'Not Received'),
(391,51,18,7,140,'Not Received'),
(392,51,26,7,91,'Not Received'),
(393,51,27,14,70,'Not Received'),
(394,51,28,7,539,'Not Received'),
(395,51,29,14,840,'Not Received'),
(396,51,32,3,228,'Not Received'),
(397,51,33,3,111,'Not Received'),
(398,51,34,3,45,'Not Received'),
(399,51,38,14,308,'Not Received'),
(400,51,41,10,400,'Not Received'),
(401,51,42,7,203,'Not Received'),
(402,51,52,7,70,'Not Received'),
(403,52,2,5,250,'Not Received'),
(404,52,4,5,750,'Not Received'),
(405,52,15,5,60,'Not Received'),
(406,52,16,1,38,'Not Received'),
(407,52,17,16,16,'Not Received'),
(408,52,18,5,100,'Not Received'),
(409,52,20,5,40,'Not Received'),
(410,52,27,10,50,'Not Received'),
(411,52,28,5,385,'Not Received'),
(412,52,29,10,600,'Not Received'),
(413,52,30,1,2,'Not Received'),
(414,52,31,5,380,'Not Received'),
(415,52,33,1,37,'Not Received'),
(416,52,34,1,15,'Not Received'),
(417,52,35,10,160,'Not Received'),
(418,52,39,1,38,'Not Received'),
(419,52,40,10,400,'Not Received'),
(420,52,42,5,145,'Not Received'),
(421,52,52,5,50,'Not Received');
/*!40000 ALTER TABLE `Order_Detailed_Part` ENABLE KEYS */;

-- 
-- Definition of Order_Details
-- 

DROP TABLE IF EXISTS `Order_Details`;
CREATE TABLE IF NOT EXISTS `Order_Details` (
  `Id_Order_Details` int NOT NULL AUTO_INCREMENT,
  `Bike_Type` varchar(45) NOT NULL,
  `Bike_Size` varchar(45) NOT NULL,
  `Bike_Color` varchar(45) NOT NULL,
  `Price` decimal(10,0) NOT NULL,
  `Bike_Status` varchar(45) NOT NULL,
  `Id_Order` int DEFAULT NULL,
  PRIMARY KEY (`Id_Order_Details`),
  KEY `FK_Order_idx` (`Id_Order`),
  KEY `FK_Type_idx` (`Bike_Type`),
  CONSTRAINT `FK_Order` FOREIGN KEY (`Id_Order`) REFERENCES `Order_Bikes` (`Id_Order`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2907 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Order_Details
-- 

/*!40000 ALTER TABLE `Order_Details` DISABLE KEYS */;
INSERT INTO `Order_Details`(`Id_Order_Details`,`Bike_Type`,`Bike_Size`,`Bike_Color`,`Price`,`Bike_Status`,`Id_Order`) VALUES
(2731,'City','28','Blue',2880,'New',314),
(2732,'City','28','Blue',2880,'New',314),
(2733,'City','28','Blue',2880,'New',314),
(2734,'City','28','Blue',2880,'New',314),
(2735,'City','28','Blue',2880,'New',314),
(2736,'City','28','Blue',2880,'New',314),
(2737,'City','28','Blue',2880,'Closed',314),
(2738,'City','28','Blue',2880,'New',314),
(2739,'City','28','Blue',2880,'Closed',314),
(2740,'City','28','Blue',2880,'New',314),
(2741,'City','28','Blue',2880,'New',314),
(2742,'City','28','Blue',2880,'New',314),
(2743,'City','26','Blue',2880,'New',315),
(2744,'City','26','Blue',2880,'New',315),
(2745,'City','26','Blue',2880,'New',316),
(2746,'City','26','Blue',2880,'New',316),
(2747,'City','26','Blue',2880,'New',316),
(2748,'City','26','Blue',2880,'New',316),
(2749,'City','26','Blue',2880,'New',316),
(2750,'City','26','Blue',2880,'New',316),
(2751,'City','26','Blue',2880,'New',316),
(2752,'City','26','Blue',2880,'New',316),
(2753,'City','26','Blue',2880,'New',316),
(2754,'City','26','Blue',2880,'New',316),
(2755,'City','26','Blue',2880,'New',316),
(2756,'City','26','Blue',2880,'Closed',316),
(2757,'City','26','Blue',2880,'New',316),
(2758,'City','26','Blue',2880,'New',316),
(2759,'City','26','Blue',2880,'Closed',316),
(2760,'City','26','Blue',2880,'New',316),
(2761,'City','26','Blue',2880,'New',316),
(2762,'City','26','Blue',2880,'Closed',316),
(2763,'City','26','Blue',2880,'New',316),
(2764,'City','26','Blue',2880,'Closed',316),
(2765,'City','26','Blue',2880,'New',316),
(2766,'City','26','Blue',2880,'New',316),
(2767,'City','26','Blue',2880,'New',316),
(2768,'City','26','Blue',2880,'New',316),
(2769,'City','26','Blue',2880,'New',316),
(2770,'City','26','Blue',2880,'New',316),
(2771,'City','26','Blue',2880,'New',316),
(2772,'City','26','Blue',2880,'New',316),
(2773,'City','26','Blue',2880,'New',316),
(2774,'City','26','Blue',2880,'New',316),
(2775,'City','26','Red',2888,'New',316),
(2776,'Adventure','28','Blue',3051,'New',317),
(2777,'Adventure','28','Blue',3051,'New',317),
(2778,'City','26','Blue',2880,'New',318),
(2779,'City','26','Blue',2880,'New',318),
(2780,'City','26','Blue',2880,'New',319),
(2781,'City','26','Blue',2880,'New',319),
(2782,'City','26','Blue',2880,'New',319),
(2783,'City','26','Blue',2880,'New',319),
(2784,'City','26','Blue',2880,'New',319),
(2785,'Adventure','26','Black',3051,'New',319),
(2786,'Adventure','26','Black',3051,'New',319),
(2787,'Adventure','26','Black',3051,'New',319),
(2788,'Adventure','26','Black',3051,'New',319),
(2789,'Adventure','26','Red',3059,'New',319),
(2790,'Adventure','26','Red',3059,'New',319),
(2791,'Adventure','26','Red',3059,'New',319),
(2792,'Adventure','26','Red',3059,'New',319),
(2793,'Adventure','26','Red',3059,'New',319),
(2794,'Adventure','26','Red',3059,'New',319),
(2795,'Adventure','26','Red',3059,'New',319),
(2796,'Adventure','26','Red',3059,'New',319),
(2797,'Adventure','26','Red',3059,'New',319),
(2798,'Adventure','26','Red',3059,'New',319),
(2799,'Adventure','26','Red',3059,'New',319),
(2800,'Adventure','26','Red',3059,'New',319),
(2801,'Adventure','26','Red',3059,'New',319),
(2802,'Adventure','26','Red',3059,'New',319),
(2803,'Adventure','26','Red',3059,'Closed',319),
(2804,'Adventure','26','Red',3059,'New',319),
(2805,'Adventure','26','Red',3059,'Closed',319),
(2806,'Adventure','26','Red',3059,'Closed',319),
(2807,'Adventure','26','Red',3059,'Closed',319),
(2808,'Adventure','26','Red',3059,'New',319),
(2809,'Explorer','28','Blue',2777,'New',320),
(2810,'Explorer','28','Blue',2777,'New',320),
(2811,'Explorer','28','Blue',2777,'New',320),
(2812,'Explorer','28','Blue',2777,'New',320),
(2813,'Explorer','28','Blue',2777,'New',320),
(2814,'Explorer','28','Blue',2777,'New',320),
(2815,'Explorer','28','Blue',2777,'New',320),
(2816,'Explorer','28','Blue',2777,'New',320),
(2817,'Explorer','28','Blue',2777,'New',320),
(2818,'Explorer','28','Blue',2777,'New',320),
(2819,'Explorer','28','Blue',2777,'New',320),
(2820,'Explorer','28','Blue',2777,'New',320),
(2821,'Explorer','28','Blue',2777,'New',320),
(2822,'Explorer','28','Blue',2777,'New',320),
(2823,'Explorer','28','Blue',2777,'New',320),
(2824,'Explorer','28','Blue',2777,'New',320),
(2825,'Explorer','28','Blue',2777,'New',320),
(2826,'Explorer','28','Blue',2777,'New',320),
(2827,'Explorer','28','Blue',2777,'New',320),
(2828,'Explorer','28','Blue',2777,'New',320),
(2829,'Explorer','28','Blue',2777,'New',320),
(2830,'Explorer','28','Blue',2777,'New',320),
(2831,'Explorer','28','Blue',2777,'New',320),
(2832,'Explorer','28','Blue',2777,'New',320),
(2833,'Explorer','28','Blue',2777,'New',320),
(2834,'City','28','Blue',887,'New',321),
(2835,'City','28','Blue',887,'New',321),
(2836,'City','28','Blue',887,'New',321),
(2837,'City','28','Blue',887,'New',321),
(2838,'City','28','Blue',887,'New',321),
(2839,'City','28','Blue',887,'New',321),
(2840,'City','28','Blue',887,'New',321),
(2841,'City','28','Blue',887,'New',321),
(2842,'City','28','Blue',887,'New',321),
(2843,'City','28','Blue',887,'New',321),
(2844,'City','26','Blue',887,'New',322),
(2845,'City','26','Blue',887,'New',322),
(2846,'City','26','Blue',887,'New',322),
(2847,'City','28','Black',887,'New',322),
(2848,'City','28','Black',887,'New',322),
(2849,'City','28','Black',887,'New',322),
(2850,'City','28','Black',887,'New',322),
(2851,'City','28','Black',887,'New',322),
(2852,'City','28','Black',887,'New',322),
(2853,'Adventure','26','Red',904,'New',322),
(2854,'Adventure','26','Red',904,'New',322),
(2855,'Adventure','26','Red',904,'New',322),
(2856,'Adventure','26','Red',904,'New',322),
(2857,'Adventure','26','Red',904,'New',322),
(2858,'Explorer','26','Black',914,'New',322),
(2859,'Explorer','26','Black',914,'New',322),
(2860,'Explorer','26','Black',914,'New',322),
(2861,'Explorer','26','Black',914,'New',322),
(2862,'City','28','Black',887,'New',323),
(2863,'City','28','Black',887,'New',323),
(2864,'City','28','Black',887,'New',323),
(2865,'City','28','Black',887,'New',323),
(2866,'City','26','Black',887,'New',324),
(2867,'City','26','Black',887,'New',324),
(2868,'City','26','Black',887,'New',324),
(2869,'City','26','Black',887,'New',324),
(2870,'City','26','Red',882,'New',324),
(2871,'City','26','Red',882,'New',324),
(2872,'City','26','Red',882,'New',324),
(2873,'City','26','Red',882,'New',324),
(2874,'Explorer','28','Blue',914,'New',324),
(2875,'Explorer','28','Blue',914,'New',324),
(2876,'Explorer','28','Blue',914,'New',324),
(2877,'Explorer','28','Blue',914,'New',324),
(2878,'City','28','Blue',887,'New',325),
(2879,'City','28','Blue',887,'New',325),
(2880,'City','28','Blue',887,'New',325),
(2881,'City','28','Black',887,'New',325),
(2882,'City','28','Black',887,'New',325),
(2883,'City','28','Black',887,'New',325),
(2884,'City','28','Red',887,'New',325),
(2885,'City','28','Red',887,'New',325),
(2886,'City','28','Red',887,'New',325),
(2887,'City','26','Red',882,'New',325),
(2888,'City','26','Red',882,'New',325),
(2889,'City','26','Red',882,'New',325),
(2890,'City','28','Black',887,'New',326),
(2891,'City','28','Black',887,'New',326),
(2892,'Adventure','26','Blue',904,'New',327),
(2893,'Adventure','26','Blue',904,'New',327),
(2894,'City','26','Black',887,'New',329),
(2895,'Adventure','26','Blue',904,'New',330),
(2896,'Adventure','26','Blue',904,'New',330),
(2897,'Adventure','26','Blue',904,'New',331),
(2898,'Adventure','26','Blue',904,'New',331),
(2899,'Adventure','26','Blue',904,'New',331),
(2900,'Explorer','26','Blue',914,'New',332),
(2901,'Explorer','26','Blue',914,'New',332),
(2902,'Explorer','26','Blue',914,'New',332),
(2903,'City','26','Blue',887,'New',333),
(2904,'Adventure','28','Black',904,'New',334),
(2905,'Adventure','28','Black',904,'New',334),
(2906,'Adventure','28','Black',904,'New',334);
/*!40000 ALTER TABLE `Order_Details` ENABLE KEYS */;

-- 
-- Definition of Order_Part
-- 

DROP TABLE IF EXISTS `Order_Part`;
CREATE TABLE IF NOT EXISTS `Order_Part` (
  `id_Order_Part` int NOT NULL AUTO_INCREMENT,
  `Week_Name` varchar(45) DEFAULT NULL,
  `Total_Price` int DEFAULT NULL,
  `Order_Date` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_Order_Part`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Order_Part
-- 

/*!40000 ALTER TABLE `Order_Part` DISABLE KEYS */;
INSERT INTO `Order_Part`(`id_Order_Part`,`Week_Name`,`Total_Price`,`Order_Date`) VALUES
(41,'G',4129,'23/03/2021 11:14:12'),
(42,'G',4129,'23/03/2021 11:15:49'),
(43,'Week : 17',8528,'23/03/2021 20:27:55'),
(44,'',4566,'26/03/2021 13:05:04'),
(45,'Week : 15',39106,'28/03/2021 17:29:33'),
(46,'Week : 15',39106,'28/03/2021 17:31:54'),
(47,'Week : 15',39106,'28/03/2021 17:33:48'),
(48,'Week : 15',39106,'28/03/2021 17:34:30'),
(49,'Week : 15',39106,'28/03/2021 17:36:22'),
(50,'Week : 15',11664,'28/03/2021 20:36:41'),
(51,'15',5017,'03/04/2021 15:16:12'),
(52,'Week : 16',3516,'03/04/2021 17:24:24');
/*!40000 ALTER TABLE `Order_Part` ENABLE KEYS */;

-- 
-- Definition of Parts
-- 

DROP TABLE IF EXISTS `Parts`;
CREATE TABLE IF NOT EXISTS `Parts` (
  `Id_Bike_Parts` int NOT NULL,
  `Bikes_Id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Parts
-- 

/*!40000 ALTER TABLE `Parts` DISABLE KEYS */;
INSERT INTO `Parts`(`Id_Bike_Parts`,`Bikes_Id`) VALUES
(1,2),
(1,3),
(1,4),
(1,5),
(1,6),
(1,7),
(1,8),
(1,9),
(1,10),
(1,11),
(1,12),
(1,13),
(1,14),
(1,15),
(1,16),
(1,17),
(1,18),
(2,2),
(2,3),
(2,4),
(2,5),
(2,6),
(2,7),
(2,8),
(2,9),
(2,10),
(2,11),
(2,12),
(2,13),
(2,14),
(2,15),
(2,16),
(2,17),
(2,18),
(3,7),
(4,2),
(4,8),
(5,3),
(5,9),
(6,4),
(6,10),
(7,5),
(7,11),
(8,6),
(8,12),
(9,13),
(10,14),
(11,15),
(12,16),
(13,17),
(14,18),
(15,2),
(15,3),
(15,4),
(15,5),
(15,6),
(15,7),
(15,8),
(15,9),
(15,10),
(15,11),
(15,12),
(15,13),
(15,14),
(15,15),
(15,16),
(15,17),
(15,18),
(16,2),
(16,3),
(16,4),
(16,5),
(16,6),
(16,7),
(16,8),
(16,9),
(16,10),
(16,11),
(16,12),
(16,13),
(16,14),
(16,15),
(16,16),
(16,17),
(16,18),
(17,2),
(17,3),
(17,4),
(17,5),
(17,6),
(17,7),
(17,8),
(17,9),
(17,10),
(17,11),
(17,12),
(17,13),
(17,14),
(17,15),
(17,16),
(17,17),
(17,18),
(18,2),
(18,3),
(18,4),
(18,5),
(18,6),
(18,7),
(18,8),
(18,9),
(18,10),
(18,11),
(18,12),
(18,13),
(18,14),
(18,15),
(18,16),
(18,17),
(18,18),
(20,2),
(21,3),
(22,4),
(23,5),
(24,6),
(25,7),
(25,8),
(25,9),
(25,13),
(25,14),
(25,15),
(26,10),
(26,11),
(26,12),
(26,16),
(26,17),
(26,18),
(27,2),
(27,3),
(27,4),
(27,5),
(27,6),
(27,7),
(27,8),
(27,9),
(27,10),
(27,11),
(27,12),
(27,13),
(27,14),
(27,15),
(27,16),
(27,17),
(27,18),
(28,2),
(28,3),
(28,4),
(28,5),
(28,6),
(28,7),
(28,8),
(28,9),
(28,10),
(28,11),
(28,12),
(28,13),
(28,14),
(28,15),
(28,16),
(28,17),
(28,18),
(29,2),
(29,3),
(29,4),
(29,5),
(29,6),
(29,7),
(29,8),
(29,9),
(29,10),
(29,11),
(29,12),
(29,13),
(29,14),
(29,15),
(29,16),
(29,17),
(29,18),
(30,2),
(30,3),
(30,4),
(30,5),
(30,6),
(30,7),
(30,8),
(30,9),
(30,10),
(30,11),
(30,12),
(31,2),
(31,3),
(31,7),
(31,8),
(31,9),
(31,13),
(31,14),
(31,15),
(32,4),
(32,5),
(32,6),
(32,10),
(32,11),
(32,12),
(32,16),
(32,17),
(32,18),
(33,2),
(33,3),
(33,4),
(33,5),
(33,6),
(33,7),
(33,8),
(33,9),
(33,10),
(33,11),
(33,12),
(33,13),
(33,14),
(33,15),
(33,16),
(33,17),
(33,18),
(34,2),
(34,3),
(34,4),
(34,5),
(34,6),
(34,7),
(34,8),
(34,9),
(34,10),
(34,11),
(34,12),
(34,13),
(34,14),
(34,15),
(34,16),
(34,17),
(34,18),
(35,1),
(35,2),
(35,3),
(36,4),
(36,5),
(36,6),
(37,7),
(37,8),
(37,9),
(37,13),
(37,14),
(37,15),
(38,10),
(38,11),
(38,12),
(38,16),
(38,17),
(38,18),
(39,2),
(39,3),
(39,4),
(39,5),
(39,6),
(39,7),
(39,8),
(39,9),
(39,10),
(39,11),
(39,12),
(40,2),
(40,3),
(40,7),
(40,8),
(40,9),
(40,13),
(40,14),
(40,15),
(41,4),
(41,5),
(41,6),
(41,10),
(41,11),
(41,12),
(41,16),
(41,17),
(41,18),
(42,2),
(42,3),
(42,4),
(42,5),
(42,6),
(42,7),
(42,8),
(42,9),
(42,10),
(42,11),
(42,12),
(42,13),
(42,14),
(42,15),
(42,16),
(42,17),
(42,18),
(52,2),
(52,3),
(52,4),
(52,5),
(52,6),
(52,7),
(52,8),
(52,9),
(52,10),
(52,11),
(52,12),
(52,13),
(52,14),
(52,15),
(52,16),
(52,17),
(52,18),
(35,1),
(17,2),
(17,2),
(17,2),
(27,2),
(29,2),
(35,2),
(40,2),
(17,3),
(17,3),
(17,3),
(27,3),
(29,3),
(35,3),
(40,3),
(17,4),
(17,4),
(17,4),
(27,4),
(29,4),
(36,4),
(41,4),
(17,5),
(17,5),
(17,5),
(27,5),
(29,5),
(36,5),
(41,5),
(17,6),
(17,6),
(17,6),
(27,6),
(29,6),
(36,6),
(41,6),
(17,7),
(17,7),
(17,7),
(27,7),
(29,7),
(37,7),
(40,7),
(17,8),
(17,8),
(17,8),
(27,8),
(29,8),
(37,8),
(40,8),
(17,9),
(17,9),
(17,9),
(27,9),
(29,9),
(37,9),
(40,9),
(17,10),
(17,10),
(17,10),
(27,10),
(29,10),
(38,10),
(41,10),
(17,11),
(17,11),
(17,11),
(27,11),
(29,11),
(38,11),
(41,11),
(17,12),
(17,12),
(17,12),
(27,12),
(29,12),
(38,12),
(41,12),
(17,13),
(17,13),
(17,13),
(27,13),
(29,13),
(37,13),
(40,13),
(17,14),
(17,14),
(17,14),
(27,14),
(29,14),
(37,14),
(40,14),
(17,15),
(17,15),
(17,15),
(27,15),
(29,15),
(37,15),
(40,15),
(17,16),
(17,16),
(17,16),
(27,16),
(29,16),
(38,16),
(41,16),
(17,17),
(17,17),
(17,17),
(27,17),
(29,17),
(38,17),
(41,17),
(17,18),
(17,18),
(17,18),
(27,18),
(29,18),
(38,18),
(41,18),
(1,1),
(2,1),
(3,1),
(15,1),
(16,1),
(17,1),
(17,1),
(17,1),
(17,1),
(18,1),
(19,1),
(27,1),
(28,1),
(29,1),
(29,1),
(31,1),
(33,1),
(34,1),
(39,1),
(40,1),
(40,1),
(42,1),
(52,1),
(1,41),
(1,41),
(2,41),
(8,41),
(15,41),
(15,41),
(16,41),
(16,41),
(17,41),
(17,41),
(17,41),
(17,41),
(18,41),
(18,41),
(24,41),
(24,41),
(27,41),
(27,41),
(28,41),
(28,41),
(29,41),
(29,41),
(30,41),
(30,41),
(32,41),
(33,41),
(33,41),
(34,41),
(36,41),
(36,41),
(41,41),
(41,41),
(42,41),
(42,41),
(52,41),
(52,41),
(30,1),
(1,41),
(2,41),
(2,41),
(2,41),
(52,41),
(52,41),
(52,41),
(42,41),
(52,41),
(52,41),
(52,41),
(42,41),
(41,41),
(42,41),
(1,41),
(1,42),
(52,42),
(2,42),
(52,42);
/*!40000 ALTER TABLE `Parts` ENABLE KEYS */;

-- 
-- Definition of Users
-- 

DROP TABLE IF EXISTS `Users`;
CREATE TABLE IF NOT EXISTS `Users` (
  `id_User` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(45) NOT NULL,
  `Role` varchar(45) NOT NULL,
  `Order` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id_User`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Users
-- 

/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users`(`id_User`,`Login`,`Role`,`Order`) VALUES
(13,'Manager','Production Manager',NULL),
(22,'Assembler1','Assembler',NULL),
(23,'Assembler2','Assembler',NULL),
(24,'Assembler3','Assembler',NULL),
(25,'Representative1','Representative',NULL);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2021-04-05 21:43:56
-- Total time: 0:0:0:0:839 (d:h:m:s:ms)
