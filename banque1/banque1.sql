-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : ven. 08 nov. 2024 à 21:05
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
-- Base de données : `banque1`
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
('81bb2250-f2ad-4cbd-81bf-d1c3107a920d', '2024110710085518', 'string', 'string', 'string', '835458745', 'user@example.com', 'string', 'F', 'USD', '1835'),
('b259ba4d-9080-4468-90d8-48c3bbbe8397', '2024110709534816', 'MESSI', 'LAPULGA', 'Lionel', '+243823078495', 'user@exampl', 'CALIFORNI', 'M', 'CDF', '1253'),
('baa30270-a0e3-4456-b69d-941bf41000f7', '2024110818183918', 'Malunda', 'Kabata', 'Tegra', '0816374467', 'tegramalundakabata01@gmail.com', 'Lemba', 'M', 'USD', '7788');

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
('56b49453-c835-44af-bcea-923f90011a51', 'baa30270-a0e3-4456-b69d-941bf41000f7', NULL, 300.0000, '2024-11-08 17:26:52'),
('896fffdc-8f78-4638-a406-2f6d1b53728a', '81bb2250-f2ad-4cbd-81bf-d1c3107a920d', 'f0f99600-3ec5-45d4-8df6-53512305cd89', 50.0000, '2024-11-08 18:33:24'),
('afdc232d-8315-46ad-86fc-d426a313b152', 'b259ba4d-9080-4468-90d8-48c3bbbe8397', NULL, 3000.0000, '2024-11-07 12:05:16');

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
('f0f99600-3ec5-45d4-8df6-53512305cd89', 'EQUITYInterbanque', 'Interbanque', 'MDgxNjM3NDQ2N21vbm51bWVyb2RldGVsZXBob25lQC8qMiglKCcoI0BefFs3ODU0XSknKSk=');

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
-- Déchargement des données de la table `retrait`
--

INSERT INTO `retrait` (`id`, `id_compte`, `from_interbanque`, `montant`, `date`) VALUES
('2cfed659-daea-4e17-9b1c-b99d3799e556', 'baa30270-a0e3-4456-b69d-941bf41000f7', NULL, 40.0000, '2024-11-08 17:29:52'),
('41bd3a36-c85b-4b94-9cd5-2b8189d8b367', 'b259ba4d-9080-4468-90d8-48c3bbbe8397', NULL, 200.0000, '2024-11-07 12:12:00'),
('af97db6c-3f84-46db-bc36-9a6cf90b6f8c', 'baa30270-a0e3-4456-b69d-941bf41000f7', 'f0f99600-3ec5-45d4-8df6-53512305cd89', 50.0000, '2024-11-08 18:33:24'),
('c83b07c4-2b18-4b0f-bdb2-2ffb7a75c97b', 'baa30270-a0e3-4456-b69d-941bf41000f7', 'f0f99600-3ec5-45d4-8df6-53512305cd89', 21.0000, '2024-11-08 19:54:21');

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
