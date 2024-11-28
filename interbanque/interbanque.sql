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
-- Base de données : `interbanque`
--

-- --------------------------------------------------------

--
-- Structure de la table `banque`
--

CREATE TABLE `banque` (
  `id` varchar(100) NOT NULL,
  `code` varchar(20) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `token` varchar(1000) NOT NULL,
  `endpoint_depot` text NOT NULL,
  `endpoint_retrait` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `banque`
--

INSERT INTO `banque` (`id`, `code`, `nom`, `token`, `endpoint_depot`, `endpoint_retrait`) VALUES
('56c60344-f5cc-4d93-9dd1-ee424ee513c7', 'BCDCInterbanque', 'BCDC', 'MDgxNjM3NDQ2N21vbm51bWVyb2RldGVsZXBob25lMUAvKjIoJSgnKCNAXnxbNzg1NF0pJykp', 'aHR0cDovL2xvY2FsaG9zdDo1MDAzL0RlcG90L2Fqb3V0ZXJfdmlhX2ludGVyYmFucXVlQC8qMiglKCcoI0BefFs3ODU0XSknKSk=', 'aHR0cDovL2xvY2FsaG9zdDo1MDAzL3JldHJhaXQvYWpvdXRlcl92aWFfaW50ZXJiYW5xdWVALyoyKCUoJygjQF58Wzc4NTRdKScpKQ=='),
('e78853c2-1ddc-4b07-92ab-dbb1cd8dc7dd', 'EQUITYInterbanque', 'EQUITY', 'MDgxNjM3NDQ2N21vbm51bWVyb2RldGVsZXBob25lQC8qMiglKCcoI0BefFs3ODU0XSknKSk=', 'aHR0cDovL2xvY2FsaG9zdDo1MDAxL0RlcG90L2Fqb3V0ZXJfdmlhX2ludGVyYmFucXVlQC8qMiglKCcoI0BefFs3ODU0XSknKSk=', 'aHR0cDovL2xvY2FsaG9zdDo1MDAxL3JldHJhaXQvYWpvdXRlcl92aWFfaW50ZXJiYW5xdWVALyoyKCUoJygjQF58Wzc4NTRdKScpKQ==');

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE `client` (
  `id` varchar(100) NOT NULL,
  `nom` varchar(20) NOT NULL,
  `postnom` varchar(20) NOT NULL,
  `prenom` varchar(20) NOT NULL,
  `email` varchar(50) NOT NULL,
  `telephone` varchar(14) NOT NULL,
  `motpasse` varchar(20) NOT NULL,
  `sexe` enum('M','F') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `client`
--

INSERT INTO `client` (`id`, `nom`, `postnom`, `prenom`, `email`, `telephone`, `motpasse`, `sexe`) VALUES
('036b9264-6790-49e9-b38b-2c6b79dc1996', 'malunda', 'Kabata', 'tegra', 'user@example.com', '0816374467', 'S2FiYXRhMDFALyoyKCUo', 'M'),
('ea23701d-05ec-42ab-b187-12b0afd91461', 'string', 'string', 'string', 'user@example.com', '835458745', 'S2FiYXRhMDJALyoyKCUo', 'M');

-- --------------------------------------------------------

--
-- Structure de la table `compte`
--

CREATE TABLE `compte` (
  `id` varchar(100) NOT NULL,
  `id_client` varchar(100) NOT NULL,
  `id_banque` varchar(100) NOT NULL,
  `numero_compte` varchar(20) NOT NULL,
  `devise` enum('CDF','USD') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `compte`
--

INSERT INTO `compte` (`id`, `id_client`, `id_banque`, `numero_compte`, `devise`) VALUES
('0b17a433-5a89-4951-8d3f-4c4f7ceaa189', '036b9264-6790-49e9-b38b-2c6b79dc1996', 'e78853c2-1ddc-4b07-92ab-dbb1cd8dc7dd', '2024110818183918', 'USD'),
('837d301e-af45-435d-9378-9fd4bde51317', 'ea23701d-05ec-42ab-b187-12b0afd91461', 'e78853c2-1ddc-4b07-92ab-dbb1cd8dc7dd', '2024110710085518', 'USD');

-- --------------------------------------------------------

--
-- Structure de la table `transfert`
--

CREATE TABLE `transfert` (
  `id` varchar(100) NOT NULL,
  `from_id_compte` varchar(100) NOT NULL,
  `to_banque` varchar(100) NOT NULL,
  `to_numero_compte` varchar(20) NOT NULL,
  `montant` decimal(20,4) NOT NULL,
  `date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `transfert`
--

INSERT INTO `transfert` (`id`, `from_id_compte`, `to_banque`, `to_numero_compte`, `montant`, `date`) VALUES
('6811ec00-2769-49b8-86c5-d45426c23e6b', '0b17a433-5a89-4951-8d3f-4c4f7ceaa189', '56c60344-f5cc-4d93-9dd1-ee424ee513c7', '2024110820053110', 21.0000, '2024-11-08 19:54:22'),
('cfeb9fc6-d753-4c79-8746-8c9ff1428b59', '0b17a433-5a89-4951-8d3f-4c4f7ceaa189', 'e78853c2-1ddc-4b07-92ab-dbb1cd8dc7dd', '2024110710085518', 50.0000, '2024-11-08 18:33:24');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `banque`
--
ALTER TABLE `banque`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `code` (`code`);

--
-- Index pour la table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `compte`
--
ALTER TABLE `compte`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_banque` (`id_banque`,`numero_compte`),
  ADD KEY `id_client` (`id_client`);

--
-- Index pour la table `transfert`
--
ALTER TABLE `transfert`
  ADD PRIMARY KEY (`id`),
  ADD KEY `from_id_compte` (`from_id_compte`),
  ADD KEY `to_banque` (`to_banque`);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `compte`
--
ALTER TABLE `compte`
  ADD CONSTRAINT `compte_ibfk_1` FOREIGN KEY (`id_client`) REFERENCES `client` (`id`),
  ADD CONSTRAINT `compte_ibfk_2` FOREIGN KEY (`id_banque`) REFERENCES `banque` (`id`);

--
-- Contraintes pour la table `transfert`
--
ALTER TABLE `transfert`
  ADD CONSTRAINT `transfert_ibfk_3` FOREIGN KEY (`from_id_compte`) REFERENCES `compte` (`id`),
  ADD CONSTRAINT `transfert_ibfk_4` FOREIGN KEY (`to_banque`) REFERENCES `banque` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
