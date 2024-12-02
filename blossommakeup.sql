-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 02-Dez-2024 às 03:46
-- Versão do servidor: 10.4.32-MariaDB
-- versão do PHP: 8.1.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `blossommakeup`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `cliente`
--

CREATE TABLE `cliente` (
  `nome` varchar(250) DEFAULT NULL,
  `cpf` varchar(250) DEFAULT NULL,
  `telefone` varchar(250) DEFAULT NULL,
  `email` varchar(250) DEFAULT NULL,
  `id` int(11) NOT NULL,
  `FK_funcionario_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `cliente`
--

INSERT INTO `cliente` (`nome`, `cpf`, `telefone`, `email`, `id`, `FK_funcionario_id`) VALUES
('Jinx', '98666432178', '19888667885', 'jinx@gmail.com', 1, NULL),
('matheus Gonzales', '67854021856', '19876453029', 'matheus@gmail.com', 2, NULL),
('João Silva', '12345678901', '31987654321', 'joao.silva@email.com', 4, 3),
('Maria Oliveira', '98765432100', '31987654322', 'maria.oliveira@email.com', 5, 6),
('Carlos Pereira', '11122233344', '31987654323', 'carlos.pereira@email.com', 6, 7),
('Fernanda Souza', '55566677788', '31987654324', 'fernanda.souza@email.com', 7, 11),
('Larissa Lima', '22233344455', '31987654326', 'larissa.lima@email.com', 9, 2),
('Eduardo Santos', '44455566677', '31987654327', 'eduardo.santos@email.com', 10, 4),
('Camila Rocha', '88899900011', '31987654328', 'camila.rocha@email.com', 11, 8),
('Felipe Almeida', '33344455566', '31987654329', 'felipe.almeida@email.com', 12, 11),
('Patricia Martins', '66677788899', '31987654330', 'patricia.martins@email.com', 13, 10);

-- --------------------------------------------------------

--
-- Estrutura da tabela `funcionario`
--

CREATE TABLE `funcionario` (
  `cpf` varchar(250) DEFAULT NULL,
  `salario` varchar(250) DEFAULT NULL,
  `cep` varchar(250) DEFAULT NULL,
  `email` varchar(250) DEFAULT NULL,
  `id` int(11) NOT NULL,
  `nome` varchar(250) DEFAULT NULL,
  `numeroCasa` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `funcionario`
--

INSERT INTO `funcionario` (`cpf`, `salario`, `cep`, `email`, `id`, `nome`, `numeroCasa`) VALUES
('98765432100', '3200', '12345001', 'bruno.souza@empresa.com', 2, 'Bruno Souza', '102'),
('45678912300', '2900', '12345002', 'carla.moraes@empresa.com', 3, 'Carla Moraes', '103'),
('65432178900', '3100', '12345003', 'diego.santos@empresa.com', 4, 'Diego Santos', '104'),
('78912345600', '3400', '12345004', 'emilia.lima@empresa.com', 5, 'Emília Lima', '105'),
('32178965400', '2800', '12345005', 'fernando.alves@empresa.com', 6, 'Fernando Alves', '106'),
('12398745600', '3300', '12345006', 'gabriela.costa@empresa.com', 7, 'Gabriela Costa', '107'),
('98732165400', '3100', '12345007', 'hugo.mendes@empresa.com', 8, 'Hugo Mendes', '108'),
('45612378900', '3000', '12345008', 'iris.carvalho@empresa.com', 9, 'Íris Carvalho', '109'),
('65498732100', '3200', '12345009', 'joao.oliveira@empresa.com', 10, 'João Oliveira', '110'),
('29076548659', '13300', '12329076', 'vitor@gmail.com', 11, 'vitor', '236'),
('0000', NULL, NULL, '785431234567', 12, 'Mariana', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `marca`
--

CREATE TABLE `marca` (
  `id` int(11) NOT NULL,
  `nome` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `marca`
--

INSERT INTO `marca` (`id`, `nome`) VALUES
(1, 'Maybelline'),
(2, 'MAC'),
(3, 'NARS'),
(4, 'Chanel'),
(5, 'Dior'),
(6, 'Urban Decay'),
(7, 'Benefit'),
(8, 'Clinique'),
(9, 'Lancôme'),
(10, 'Estée Lauder'),
(11, 'MariMaria');

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto`
--

CREATE TABLE `produto` (
  `id` int(11) NOT NULL,
  `preco` float DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `nome` varchar(255) DEFAULT NULL,
  `FK_funcionario_id` int(11) DEFAULT NULL,
  `FK_tipo_id` int(11) DEFAULT NULL,
  `FK_marca_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `produto`
--

INSERT INTO `produto` (`id`, `preco`, `quantidade`, `nome`, `FK_funcionario_id`, `FK_tipo_id`, `FK_marca_id`) VALUES
(2, 29.99, 100, 'Nars Afterglow Lip Shine 5,5ml', 6, 10, 3),
(4, 35, 200, 'Urban Decay All Nighter Setting Spray 118ml', 2, 3, 4),
(5, 23.75, 80, 'Too Faced Better Than Sex Mascara 8g', 2, 7, 10),
(6, 21.99, 120, 'Anastasia Beverly Hills Brow Wiz 0,085g', 3, 4, 1),
(7, 19.99, 180, 'Maybelline Fit Me Matte + Poreless Foundation 30ml', 4, 1, 5),
(8, 36, 90, 'Fenty Beauty Pro Filt\'r Soft Matte Longwear Foundation 32ml', 4, 1, 6),
(9, 34.5, 130, 'Charlotte Tilbury Pillow Talk Lipstick 3,5g', 5, 9, 7),
(11, 30, 140, 'Benefit Hoola Matte Bronzer 8g', 6, 5, 9),
(12, 1000, 25, 'Primer Glow', NULL, 5, 6),
(13, 45, 48, 'Base Vegana', 3, 1, 11);

-- --------------------------------------------------------

--
-- Estrutura da tabela `tipo`
--

CREATE TABLE `tipo` (
  `id` int(11) NOT NULL,
  `nome` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tipo`
--

INSERT INTO `tipo` (`id`, `nome`) VALUES
(1, 'Base'),
(3, 'Pó Compacto'),
(4, 'Blush'),
(5, 'Iluminador'),
(6, 'Sombra'),
(7, 'Rímel'),
(8, 'Delineador'),
(9, 'Batom'),
(10, 'Gloss'),
(11, 'Contorno');

-- --------------------------------------------------------

--
-- Estrutura da tabela `venda`
--

CREATE TABLE `venda` (
  `id` int(11) NOT NULL,
  `valor` float DEFAULT NULL,
  `FK_funcionario_id` int(11) DEFAULT NULL,
  `FK_cliente_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `vendaproduto`
--

CREATE TABLE `vendaproduto` (
  `FK_venda_id` int(11) DEFAULT NULL,
  `FK_produto_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_cliente_1` (`FK_funcionario_id`);

--
-- Índices para tabela `funcionario`
--
ALTER TABLE `funcionario`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `marca`
--
ALTER TABLE `marca`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `produto`
--
ALTER TABLE `produto`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_produto_1` (`FK_funcionario_id`),
  ADD KEY `FK_produto_2` (`FK_tipo_id`),
  ADD KEY `FK_produto_3` (`FK_marca_id`);

--
-- Índices para tabela `tipo`
--
ALTER TABLE `tipo`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `venda`
--
ALTER TABLE `venda`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_venda_1` (`FK_funcionario_id`),
  ADD KEY `FK_venda_2` (`FK_cliente_id`);

--
-- Índices para tabela `vendaproduto`
--
ALTER TABLE `vendaproduto`
  ADD KEY `FK_vendaProduto_0` (`FK_venda_id`),
  ADD KEY `FK_vendaProduto_1` (`FK_produto_id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `cliente`
--
ALTER TABLE `cliente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de tabela `funcionario`
--
ALTER TABLE `funcionario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de tabela `marca`
--
ALTER TABLE `marca`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de tabela `produto`
--
ALTER TABLE `produto`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de tabela `tipo`
--
ALTER TABLE `tipo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de tabela `venda`
--
ALTER TABLE `venda`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `cliente`
--
ALTER TABLE `cliente`
  ADD CONSTRAINT `FK_cliente_1` FOREIGN KEY (`FK_funcionario_id`) REFERENCES `funcionario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limitadores para a tabela `produto`
--
ALTER TABLE `produto`
  ADD CONSTRAINT `FK_produto_1` FOREIGN KEY (`FK_funcionario_id`) REFERENCES `funcionario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_produto_2` FOREIGN KEY (`FK_tipo_id`) REFERENCES `tipo` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_produto_3` FOREIGN KEY (`FK_marca_id`) REFERENCES `marca` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limitadores para a tabela `venda`
--
ALTER TABLE `venda`
  ADD CONSTRAINT `FK_venda_1` FOREIGN KEY (`FK_funcionario_id`) REFERENCES `funcionario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_venda_2` FOREIGN KEY (`FK_cliente_id`) REFERENCES `cliente` (`id`) ON DELETE CASCADE;

--
-- Limitadores para a tabela `vendaproduto`
--
ALTER TABLE `vendaproduto`
  ADD CONSTRAINT `FK_vendaProduto_0` FOREIGN KEY (`FK_venda_id`) REFERENCES `venda` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_vendaProduto_1` FOREIGN KEY (`FK_produto_id`) REFERENCES `produto` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
