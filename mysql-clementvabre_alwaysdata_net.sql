-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: mysql-clementvabre.alwaysdata.net
-- Generation Time: Apr 09, 2024 at 07:04 PM
-- Server version: 10.6.17-MariaDB
-- PHP Version: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `clementvabre_contact`
--
CREATE DATABASE IF NOT EXISTS `clementvabre_contact` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `clementvabre_contact`;

-- --------------------------------------------------------

--
-- Table structure for table `contact`
--

CREATE TABLE `contact` (
  `id_contact` int(11) NOT NULL,
  `prenom` varchar(50) DEFAULT NULL,
  `nom` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `tel` varchar(30) DEFAULT NULL,
  `status` enum('famille','ami','travail') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contact`
--

INSERT INTO `contact` (`id_contact`, `prenom`, `nom`, `email`, `tel`, `status`) VALUES
(22, 'Bobs', 'Brown', 'bob@example.com', '111111112', 'ami'),
(23, 'Emily', 'Jones', 'emily@example.com', '222222222', 'famille'),
(25, 'Laura', 'Taylor', 'laura@example.com', '777777777', 'ami'),
(26, 'Kevin', 'Anderson', 'kevin@example.com', '888888888', 'travail'),
(38, 'clement', 'vabre', 'clemvabre@gmail.com', '0783662160', 'famille'),
(39, 'Alice', 'Smith', 'alice.smith@example.com', '0123456789', 'ami'),
(40, 'Bob', 'Johnson', 'bob.johnson@example.com', '0123456789', 'famille'),
(41, 'Claire', 'Dupont', 'claire.dupont@example.com', '0123456789', 'travail'),
(42, 'David', 'Martin', 'david.martin@example.com', '0123456789', 'ami'),
(43, 'Emily', 'Jones', 'emily.jones@example.com', '0123456789', 'famille'),
(44, 'Frank', 'Garcia', 'frank.garcia@example.com', '0123456789', 'travail'),
(45, 'Grace', 'Lee', 'grace.lee@example.com', '0123456789', 'ami'),
(46, 'Henry', 'Rodriguez', 'henry.rodriguez@example.com', '0123456789', 'famille'),
(47, 'Isabella', 'Martinez', 'isabella.martinez@example.com', '0123456789', 'travail'),
(48, 'Jack', 'Hernandez', 'jack.hernandez@example.com', '0123456789', 'ami'),
(49, 'Kelly', 'Walker', 'kelly.walker@example.com', '0123456789', 'famille'),
(50, 'Liam', 'Young', 'liam.young@example.com', '0123456789', 'travail'),
(51, 'Mia', 'Allen', 'mia.allen@example.com', '0123456789', 'ami'),
(52, 'Nathan', 'Lewis', 'nathan.lewis@example.com', '0123456789', 'famille'),
(53, 'Olivia', 'King', 'olivia.king@example.com', '0123456789', 'travail'),
(54, 'Patrick', 'Baker', 'patrick.baker@example.com', '0123456789', 'ami'),
(55, 'Quinn', 'Young', 'quinn.young@example.com', '0123456789', 'famille'),
(56, 'Rachel', 'Perez', 'rachel.perez@example.com', '0123456789', 'travail'),
(57, 'Samuel', 'Scott', 'samuel.scott@example.com', '0123456789', 'ami'),
(58, 'Taylor', 'Green', 'taylor.green@example.com', '0123456789', 'famille'),
(59, 'remi', 'vabre', 'remvabrea', '068543544', 'famille');

-- --------------------------------------------------------

--
-- Table structure for table `contact_reseaux_sociaux`
--

CREATE TABLE `contact_reseaux_sociaux` (
  `id_contact` int(11) NOT NULL,
  `id_reseau` int(11) NOT NULL,
  `pseudo` varchar(50) DEFAULT NULL,
  `url` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contact_reseaux_sociaux`
--

INSERT INTO `contact_reseaux_sociaux` (`id_contact`, `id_reseau`, `pseudo`, `url`) VALUES
(22, 1, 'ttt', NULL),
(22, 2, 'test', 'test'),
(22, 3, NULL, NULL),
(22, 4, NULL, NULL),
(22, 5, NULL, NULL),
(23, 1, NULL, NULL),
(23, 2, NULL, NULL),
(23, 3, NULL, NULL),
(23, 4, NULL, NULL),
(23, 5, NULL, NULL),
(25, 1, 'layua', 'aaaaa'),
(25, 2, NULL, NULL),
(25, 3, NULL, NULL),
(25, 4, NULL, NULL),
(25, 5, NULL, NULL),
(26, 1, NULL, NULL),
(26, 2, NULL, NULL),
(26, 3, NULL, NULL),
(26, 4, NULL, NULL),
(26, 5, NULL, NULL),
(38, 1, NULL, NULL),
(38, 2, NULL, NULL),
(38, 3, NULL, NULL),
(38, 4, NULL, NULL),
(38, 5, NULL, NULL),
(39, 1, NULL, NULL),
(39, 2, NULL, NULL),
(39, 3, NULL, NULL),
(39, 4, NULL, NULL),
(39, 5, NULL, NULL),
(40, 1, NULL, NULL),
(40, 2, NULL, NULL),
(40, 3, NULL, NULL),
(40, 4, NULL, NULL),
(40, 5, NULL, NULL),
(41, 1, NULL, NULL),
(41, 2, NULL, NULL),
(41, 3, NULL, NULL),
(41, 4, NULL, NULL),
(41, 5, NULL, NULL),
(42, 1, NULL, NULL),
(42, 2, NULL, NULL),
(42, 3, NULL, NULL),
(42, 4, NULL, NULL),
(42, 5, NULL, NULL),
(43, 1, NULL, NULL),
(43, 2, NULL, NULL),
(43, 3, NULL, NULL),
(43, 4, NULL, NULL),
(43, 5, NULL, NULL),
(44, 1, NULL, NULL),
(44, 2, NULL, NULL),
(44, 3, NULL, NULL),
(44, 4, NULL, NULL),
(44, 5, NULL, NULL),
(45, 1, NULL, NULL),
(45, 2, NULL, NULL),
(45, 3, NULL, NULL),
(45, 4, NULL, NULL),
(45, 5, NULL, NULL),
(46, 1, NULL, NULL),
(46, 2, NULL, NULL),
(46, 3, NULL, NULL),
(46, 4, NULL, NULL),
(46, 5, NULL, NULL),
(47, 1, NULL, NULL),
(47, 2, NULL, NULL),
(47, 3, NULL, NULL),
(47, 4, NULL, NULL),
(47, 5, NULL, NULL),
(48, 1, NULL, NULL),
(48, 2, NULL, NULL),
(48, 3, NULL, NULL),
(48, 4, NULL, NULL),
(48, 5, NULL, NULL),
(49, 1, NULL, NULL),
(49, 2, NULL, NULL),
(49, 3, NULL, NULL),
(49, 4, NULL, NULL),
(49, 5, NULL, NULL),
(50, 1, NULL, NULL),
(50, 2, NULL, NULL),
(50, 3, NULL, NULL),
(50, 4, NULL, NULL),
(50, 5, NULL, NULL),
(51, 1, NULL, NULL),
(51, 2, NULL, NULL),
(51, 3, NULL, NULL),
(51, 4, NULL, NULL),
(51, 5, NULL, NULL),
(52, 1, NULL, NULL),
(52, 2, NULL, NULL),
(52, 3, NULL, NULL),
(52, 4, NULL, NULL),
(52, 5, NULL, NULL),
(53, 1, NULL, NULL),
(53, 2, NULL, NULL),
(53, 3, NULL, NULL),
(53, 4, NULL, NULL),
(53, 5, NULL, NULL),
(54, 1, NULL, NULL),
(54, 2, NULL, NULL),
(54, 3, NULL, NULL),
(54, 4, NULL, NULL),
(54, 5, NULL, NULL),
(55, 1, NULL, NULL),
(55, 2, NULL, NULL),
(55, 3, NULL, NULL),
(55, 4, NULL, NULL),
(55, 5, NULL, NULL),
(56, 1, NULL, NULL),
(56, 2, NULL, NULL),
(56, 3, NULL, NULL),
(56, 4, NULL, NULL),
(56, 5, NULL, NULL),
(57, 1, NULL, NULL),
(57, 2, NULL, NULL),
(57, 3, NULL, NULL),
(57, 4, NULL, NULL),
(57, 5, NULL, NULL),
(58, 1, NULL, NULL),
(58, 2, NULL, NULL),
(58, 3, NULL, NULL),
(58, 4, NULL, NULL),
(58, 5, NULL, NULL),
(59, 1, NULL, NULL),
(59, 2, 'ff', 'ff'),
(59, 3, NULL, NULL),
(59, 4, NULL, NULL),
(59, 5, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `reseaux_sociaux_list`
--

CREATE TABLE `reseaux_sociaux_list` (
  `id_reseau` int(11) NOT NULL,
  `nom_reseau` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reseaux_sociaux_list`
--

INSERT INTO `reseaux_sociaux_list` (`id_reseau`, `nom_reseau`) VALUES
(1, 'youtube'),
(2, 'X'),
(3, 'snapchat'),
(4, 'insta'),
(5, 'WhatsApp');

-- --------------------------------------------------------

--
-- Table structure for table `tache`
--

CREATE TABLE `tache` (
  `idtache` int(11) NOT NULL,
  `fait` tinyint(1) NOT NULL DEFAULT 0,
  `temps` time DEFAULT NULL,
  `Lieux` varchar(40) DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `todolist_idtodolist` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tache`
--

INSERT INTO `tache` (`idtache`, `fait`, `temps`, `Lieux`, `Description`, `todolist_idtodolist`) VALUES
(13, 0, '12:00:00', 'US', 'voyage 1 semaine', 4),
(52, 0, '10:00:00', 'geneve', 'prendre avion', 4),
(58, 0, '10:00:00', 'st michel', 'venir a 10:10', 16);

-- --------------------------------------------------------

--
-- Table structure for table `todolist`
--

CREATE TABLE `todolist` (
  `idtodolist` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `todolist`
--

INSERT INTO `todolist` (`idtodolist`, `name`) VALUES
(4, 'voyage'),
(12, 'tgf'),
(15, 'rdv'),
(16, 'cours jeudi');

-- --------------------------------------------------------

--
-- Table structure for table `Utilisateurs`
--

CREATE TABLE `Utilisateurs` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `Utilisateurs`
--

INSERT INTO `Utilisateurs` (`id`, `username`, `password`) VALUES
(4, 'quen', '49f6d99200d4cdde9e89ba02c0933eca'),
(7, 'clem', '49c040447d066cb774f12f6134ff0b7d'),
(8, 'alex', '534b44a19bf18d20b71ecc4eb77c572f'),
(9, 'yael', '08d43b10db9849931c06cdf9c10e38a1'),
(10, 'daniel', '81dc9bdb52d04dc20036dbd8313ed055'),
(13, 'venki', '81dc9bdb52d04dc20036dbd8313ed055');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `contact`
--
ALTER TABLE `contact`
  ADD PRIMARY KEY (`id_contact`);

--
-- Indexes for table `contact_reseaux_sociaux`
--
ALTER TABLE `contact_reseaux_sociaux`
  ADD PRIMARY KEY (`id_contact`,`id_reseau`),
  ADD KEY `id_reseau` (`id_reseau`);

--
-- Indexes for table `reseaux_sociaux_list`
--
ALTER TABLE `reseaux_sociaux_list`
  ADD PRIMARY KEY (`id_reseau`);

--
-- Indexes for table `tache`
--
ALTER TABLE `tache`
  ADD PRIMARY KEY (`idtache`),
  ADD KEY `fk_tache_todolist1_idx` (`todolist_idtodolist`);

--
-- Indexes for table `todolist`
--
ALTER TABLE `todolist`
  ADD PRIMARY KEY (`idtodolist`);

--
-- Indexes for table `Utilisateurs`
--
ALTER TABLE `Utilisateurs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `contact`
--
ALTER TABLE `contact`
  MODIFY `id_contact` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;

--
-- AUTO_INCREMENT for table `reseaux_sociaux_list`
--
ALTER TABLE `reseaux_sociaux_list`
  MODIFY `id_reseau` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `tache`
--
ALTER TABLE `tache`
  MODIFY `idtache` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=59;

--
-- AUTO_INCREMENT for table `todolist`
--
ALTER TABLE `todolist`
  MODIFY `idtodolist` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `Utilisateurs`
--
ALTER TABLE `Utilisateurs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `contact_reseaux_sociaux`
--
ALTER TABLE `contact_reseaux_sociaux`
  ADD CONSTRAINT `contact_reseaux_sociaux_ibfk_1` FOREIGN KEY (`id_contact`) REFERENCES `contact` (`id_contact`),
  ADD CONSTRAINT `contact_reseaux_sociaux_ibfk_2` FOREIGN KEY (`id_reseau`) REFERENCES `reseaux_sociaux_list` (`id_reseau`);

--
-- Constraints for table `tache`
--
ALTER TABLE `tache`
  ADD CONSTRAINT `fk_tache_todolist1` FOREIGN KEY (`todolist_idtodolist`) REFERENCES `todolist` (`idtodolist`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
