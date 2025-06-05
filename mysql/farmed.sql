CREATE DATABASE IF NOT EXISTS farmed;
USE farmed;

CREATE TABLE IF NOT EXISTS usuarios(
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(100) NOT NULL,
    rol ENUM('admin', 'usuario') DEFAULT 'usuario',
    activo BOOLEAN DEFAULT TRUE,
    fechaRegistro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE IF NOT EXISTS medicamentos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10, 2) NOT NULL,
    categoria VARCHAR(100),
    stock INT NOT NULL,
    disponible BOOLEAN DEFAULT TRUE,
    imagen VARCHAR(255), 
    fechaAgregado TIMESTAMP DEFAULT current_timestamp
);

CREATE TABLE IF NOT EXISTS compras (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuarioId INT NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10, 2) NOT NULL,
    pagado BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (usuarioId) REFERENCES usuarios(id)
);

CREATE TABLE IF NOT EXISTS detalle_compra(
    id INT AUTO_INCREMENT PRIMARY KEY,
    compraId INT NOT NULL,
    medicamentoId INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (compraId) REFERENCES compras(id),
    FOREIGN KEY (medicamentoId) REFERENCES medicamentos(id)
);

-- INSERT INTO medicamentos(id, nombre, descripcion, precio, categoria, stock, disponible, imagen, fechaAgregado) VALUES(
--    (1, "Paracetamol 500mg", "Analgésico y antipirético para el alivio del dolor leve o moderado y estados febriles.", 4.95, "Medicamentos", 10, TRUE, "https://www.farmaciasdirect.es/cdn/shop/files/paracetamol-normon-500-mg-20-comprimidos.webp?v=1725054603", CURRENT_TIMESTAMP),
--    (2, "Ibuprofeno 600mg", "Antiinflamatorio no esteroideo (AINE) para el tratamiento del dolor y la inflamación.", 5.99, "Medicamentos", 10, TRUE, "https://www.kernpharma.com/sites/default/files/styles/max_575x575/public/productos/imagenes/ibuprofeno-kern-pharma-600-mg-comprimidos-recubiertos-envase-clinico-6337.jpg?itok=oHEo1vrh", CURRENT_TIMESTAMP)
-- );

INSERT INTO medicamentos(nombre, descripcion, precio, categoria, stock, disponible, imagen, fechaAgregado) VALUES
("Paracetamol 500mg", "Analgésico y antipirético para el alivio del dolor leve o moderado y estados febriles.", 4.95, "Medicamentos", 10, TRUE, "https://www.farmaciasdirect.es/cdn/shop/files/paracetamol-normon-500-mg-20-comprimidos.webp?v=1725054603", CURRENT_TIMESTAMP),
("Ibuprofeno 600mg", "Antiinflamatorio no esteroideo (AINE) para el tratamiento del dolor y la inflamación.", 5.99, "Medicamentos", 10, TRUE, "https://www.kernpharma.com/sites/default/files/styles/max_575x575/public/productos/imagenes/ibuprofeno-kern-pharma-600-mg-comprimidos-recubiertos-envase-clinico-6337.jpg?itok=oHEo1vrh", CURRENT_TIMESTAMP),
("Amoxicilina 500mg", "Antibiótico de amplio espectro utilizado en infecciones bacterianas.", 6.80, "Medicamentos", 15, TRUE, "https://botplusweb.farmaceuticos.com/Documentos/AEMPS/Fotos/267829/full/62586_materialas.jpg", CURRENT_TIMESTAMP),
("Loratadina 10mg", "Antihistamínico para tratar alergias estacionales y crónicas.", 3.50, "Medicamentos", 25, TRUE, "https://botplusweb.farmaceuticos.com/Documentos/AEMPS/Fotos/107503/full/63696_materialas_1.jpg", CURRENT_TIMESTAMP),
("Omeprazol 20mg", "Inhibidor de la bomba de protones utilizado para tratar el reflujo gástrico.", 4.20, "Medicamentos", 20, TRUE, "https://imagenes.heraldo.es/files/image_1920_1080/uploads/imagenes/2021/11/27/omeprazol.jpeg", CURRENT_TIMESTAMP);

INSERT INTO medicamentos(nombre, descripcion, precio, categoria, stock, disponible, imagen, fechaAgregado) VALUES
("Crema Hidratante Facial", "Crema hidratante para piel sensible con ácido hialurónico.", 12.50, "Dermocosmética", 20, TRUE, "https://img.freepik.com/psd-gratis/contenedor-crema-levitante-estudio-bodegon_23-2150807017.jpg", CURRENT_TIMESTAMP),
("Protector Solar SPF50", "Protección solar alta para rostro y cuerpo, resistente al agua.", 14.90, "Dermocosmética", 30, TRUE, "https://media-pierre-fabre.wedia-group.com/api/wedia/dam/transform/u5wa3z31qn5se8zaxwqp7kemidz43diywws4kqe/pf_square/u5wa3z31qn5se8zaxwqp7kemidz43diywws4kqe?t=resize&width=800&height=800", CURRENT_TIMESTAMP),
("Gel Limpiador Facial", "Limpia profundamente sin resecar la piel. Ideal para piel grasa.", 9.75, "Dermocosmética", 25, TRUE, "https://www.cerave.pe/-/media/project/loreal/brand-sites/cerave/americas/pe/new_product/new_product_700x875/limpieza/gel-limpiador-espumoso-473ml/3337875597357.jpg?rev=-1?w=500&hash=8087A4F4D132AE8CD77637731F0E9AE0", CURRENT_TIMESTAMP),
("Crema Antiarrugas", "Crema rejuvenecedora con retinol y colágeno.", 18.90, "Dermocosmética", 18, TRUE, "https://e00-telva.uecdn.es/assets/multimedia/imagenes/2020/02/13/15816002774294.jpg", CURRENT_TIMESTAMP),
("Serum Vitamina C", "Ilumina la piel y combate los signos del envejecimiento.", 22.00, "Dermocosmética", 12, TRUE, "https://camaleoncosmetics.com/477-large_default/serum-concentrado-vitamina-c.jpg", CURRENT_TIMESTAMP);

INSERT INTO medicamentos(nombre, descripcion, precio, categoria, stock, disponible, imagen, fechaAgregado) VALUES
("Pasta Dental Anticaries", "Con flúor para protección completa contra caries.", 3.75, "Higiene Bucal", 50, TRUE, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScQ6rp8eQgAEI5Hls5YhtQLmszW4jNhyDF2A&s", CURRENT_TIMESTAMP),
("Enjuague Bucal Antiséptico", "Ayuda a eliminar bacterias y mantener el aliento fresco.", 5.60, "Higiene Bucal", 30, TRUE, "https://www.farmaciamoron.com/190-large_default/enjuague-bucal-clorhexidina-005-perio-aid.jpg", CURRENT_TIMESTAMP),
("Cepillo Dental Suave", "Cepillo con cerdas suaves ideal para encías sensibles.", 2.40, "Higiene Bucal", 60, TRUE, "https://images.ctfassets.net/tdc24j4ik3zq/2w8gKlB93j9V2DpiiVwtiZ/6c818cf88567ff058a945acdde1ca82b/file.png", CURRENT_TIMESTAMP),
("Hilo Dental", "Higiene interdental para evitar caries y placa.", 1.90, "Higiene Bucal", 45, TRUE, "https://images.ctfassets.net/tdc24j4ik3zq/54kEstzkNV7nAHt9gD2SaL/8f4a6d92aa2a17b3991d65bb936658ca/file.png", CURRENT_TIMESTAMP),
("Kit de Blanqueamiento Dental", "Blanqueador casero seguro para dientes más blancos.", 19.99, "Higiene Bucal", 15, TRUE, "https://cdn2.primor.eu/media/catalog/product/cache/8d3aba296f7a18b5251ee30fa5db42b2/R/a/RapidWhite_BleachingPower_Set_IBERIA_Mockup__9__9af2.jpg", CURRENT_TIMESTAMP);

INSERT INTO medicamentos(nombre, descripcion, precio, categoria, stock, disponible, imagen, fechaAgregado) VALUES
("Vendas Adhesivas", "Vendas de distintos tamaños para pequeñas heridas.", 2.40, "Primeros Auxilios", 100, TRUE, "https://www.dhmaterialmedico.com/material-medico/fotos/1-1-venda-elastica-adhesiva-6cm-x-25m-unitaria-encelofanada-1721889968.png", CURRENT_TIMESTAMP),
("Agua Oxigenada", "Solución desinfectante para limpiar heridas.", 1.80, "Primeros Auxilios", 60, TRUE, "https://botiquinsans.com/wp-content/uploads/2022/11/Agua_oxigenada_Montplet_10.jpg", CURRENT_TIMESTAMP),
("Alcohol Etílico 70%", "Desinfectante para piel intacta antes de inyecciones.", 2.10, "Primeros Auxilios", 70, TRUE, "https://www.acofarma.com/catalogo/wp-content/uploads/sites/2/2020/03/1527912_alcohol_70.jpg", CURRENT_TIMESTAMP),
("Gasas Estériles", "Gasas estériles para cubrir y proteger heridas.", 3.25, "Primeros Auxilios", 80, TRUE, "https://www.dhmaterialmedico.com/material-medico/fotos/gasa-de-algodon-esteril-15-hilos-8-capas-plegada-a-75x6-sobre-de-5-uds-1701680138.png", CURRENT_TIMESTAMP),
("Tijeras de Emergencia", "Tijeras médicas para cortes de emergencia.", 5.50, "Primeros Auxilios", 20, TRUE, "https://iberomed.es/129498-producto_ficha/tijera-corta-ropa.jpg", CURRENT_TIMESTAMP);

