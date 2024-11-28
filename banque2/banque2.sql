-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : ven. 08 nov. 2024 à 21:06
-- Version du serveur : 10.4.32-MariaDB
-- Version de PHP : 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `banque2`
--

-- --------------------------------------------------------

--
-- Structure de la table `compte`
--

CREATE TABLE `compte` (
  `id` varchar(100) NOT NULL,
  `numero_compte` varchar(20) NOT NULL,
  `nom` varchar(20) NOT NULL,
  `postnom` varchar(20) NOT NULL,
  `prenom` varchar(20) NOT NULL,
  `telephone` varchar(14) NOT NULL,
  `email` varchar(50) NOT NULL,
  `adresse` varchar(300) NOT NULL,
  `sexe` enum('M','F') NOT NULL,
  `devise` enum('CDF','USD') NOT NULL,
  `code_pin` varchar(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `compte`
--

INSERT INTO `compte` (`id`, `numero_compte`, `nom`, `postnom`, `prenom`, `telephone`, `email`, `adresse`, `sexe`, `devise`, `code_pin`) VALUES
('d6ac75f8-325d-4e28-b956-db9a36eb4c9f', '2024110820053110', 'moz', 'do', 'grace', '0821043504', 'user@example.com', 'string', 'M', 'USD', '5533');

-- --------------------------------------------------------

--
-- Structure de la table `depot`
--

CREATE TABLE `depot` (
  `id` varchar(100) NOT NULL,
  `id_compte` varchar(100) NOT NULL,
  `from_interbanque` varchar(100) DEFAULT NULL,
  `montant` decimal(20,4) NOT NULL,
  `date` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `depot`
--

INSERT INTO `depot` (`id`, `id_compte`, `from_interbanque`, `montant`, `date`) VALUES
('e31c3802-7c1c-43ba-b134-82852c076564', 'd6ac75f8-325d-4e28-b956-db9a36eb4c9f', 'c7714542-41a1-4819-a0fc-69986b6e755a', 21.0000, '2024-11-08 19:54:22');

-- --------------------------------------------------------

--
-- Structure de la table `interbanque`
--

CREATE TABLE `interbanque` (
  `id` varchar(100) NOT NULL,
  `code` varchar(20) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `token` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `interbanque`
--

INSERT INTO `interbanque` (`id`, `code`, `nom`, `token`) VALUES
('c7714542-41a1-4819-a0fc-69986b6e755a', 'BCDCInterbanque', 'Interbanque', 'MDgxNjM3NDQ2N21vbm51bWVyb2RldGVsZXBob25lMUAvKjIoJSgnKCNAXnxbNzg1NF0pJykp');

-- --------------------------------------------------------

--
-- Structure de la table `retrait`
--

CREATE TABLE `retrait` (
  `id` varchar(100) NOT NULL,
  `id_compte` varchar(100) NOT NULL,
  `from_interbanque` varchar(100) DEFAULT NULL,
  `montant` decimal(20,4) NOT NULL,
  `date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `compte`
--
ALTER TABLE `compte`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `depot`
--
ALTER TABLE `depot`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_compte` (`id_compte`),
  ADD KEY `from_interbanque` (`from_interbanque`);

--
-- Index pour la table `interbanque`
--
ALTER TABLE `interbanque`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `code` (`code`);

--
-- Index pour la table `retrait`
--
ALTER TABLE `retrait`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_compte` (`id_compte`),
  ADD KEY `from_interbanque` (`from_interbanque`);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `depot`
--
ALTER TABLE `depot`
  ADD CONSTRAINT `depot_ibfk_1` FOREIGN KEY (`id_compte`) REFERENCES `compte` (`id`),
  ADD CONSTRAINT `depot_ibfk_2` FOREIGN KEY (`from_interbanque`) REFERENCES `interbanque` (`id`);

--
-- Contraintes pour la table `retrait`
--
ALTER TABLE `retrait`
  ADD CONSTRAINT `retrait_ibfk_1` FOREIGN KEY (`id_compte`) REFERENCES `compte` (`id`),
  ADD CONSTRAINT `retrait_ibfk_2` FOREIGN KEY (`from_interbanque`) REFERENCES `interbanque` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
