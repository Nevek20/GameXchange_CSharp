-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 11/02/2025 às 15:57
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `bd_jogos`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `tb_games`
--

CREATE TABLE `tb_games` (
  `Id` int(11) NOT NULL,
  `Titulo` varchar(255) NOT NULL,
  `Avaliacao` varchar(2) NOT NULL,
  `Tamanho` varchar(5) NOT NULL,
  `Descricao` varchar(255) NOT NULL,
  `Valor` varchar(8) NOT NULL,
  `Desenvolvedor` varchar(100) NOT NULL,
  `Genero` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `tb_games`
--

INSERT INTO `tb_games` (`Id`, `Titulo`, `Avaliacao`, `Tamanho`, `Descricao`, `Valor`, `Desenvolvedor`, `Genero`) VALUES
(1, 'asdasd', '10', '555GB', 'asdasdsad', 'R$123,12', 'dasdsadasd', '4554654');

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `tb_games`
--
ALTER TABLE `tb_games`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `tb_games`
--
ALTER TABLE `tb_games`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
