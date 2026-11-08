USE [4vGym];
GO

-- 0. Limpiar datos previos si existieran (en orden para respetar las claves foráneas)
DELETE FROM [ActivityInstructor];
DELETE FROM [Activities];
DELETE FROM [ActivityTypes];
DELETE FROM [Instructors];
GO

-- 1. Insertar Tipos de Actividad
SET IDENTITY_INSERT [ActivityTypes] ON;
INSERT INTO [ActivityTypes] ([Id], [Name], [NumberMonitors]) VALUES 
(1, 'BodyPump', 2),
(2, 'Spinning', 1),
(3, 'Pilates', 1),
(4, 'CrossFit', 3),
(5, 'Yoga', 1),
(6, 'Zumba', 2),
(7, 'Aquagym', 2),
(8, 'Boxeo', 2);
SET IDENTITY_INSERT [ActivityTypes] OFF;
GO

-- 2. Insertar Instructores (formato nombre.urlfoto)
SET IDENTITY_INSERT [Instructors] ON;
INSERT INTO [Instructors] ([Id], [Name], [Email], [PhotoUrl], [Phone]) VALUES
(1, 'Miguel Goyena', 'miguel_goyena@cuatrovientos.org', 'https://4vgym.es/miguel_goyena.urlfoto', '654121212'),
(2, 'María Martín', 'maria_martin@cuatrovientos.org', 'https://4vgym.es/maria_martin.urlfoto', '663423121'),
(3, 'Iban Sarría', 'iban_sarria@cuatrovientos.org', 'https://4vgym.es/iban_sarria.urlfoto', '654646464'),
(4, 'Laura Gómez', 'laura_gomez@cuatrovientos.org', 'https://4vgym.es/laura_gomez.urlfoto', '611223344'),
(5, 'Carlos Ruiz', 'carlos_ruiz@cuatrovientos.org', 'https://4vgym.es/carlos_ruiz.urlfoto', '677889900'),
(6, 'Ana Beltrán', 'ana_beltran@cuatrovientos.org', 'https://4vgym.es/ana_beltran.urlfoto', '688990011'),
(7, 'David Sanz', 'david_sanz@cuatrovientos.org', 'https://4vgym.es/david_sanz.urlfoto', '622334455'),
(8, 'Elena Morales', 'elena_morales@cuatrovientos.org', 'https://4vgym.es/elena_morales.urlfoto', '633445566');
SET IDENTITY_INSERT [Instructors] OFF;
GO

-- 3. Insertar Actividades (Fechas futuras válidas para el aforo y validaciones)
SET IDENTITY_INSERT [Activities] ON;
INSERT INTO [Activities] ([Id], [ActivityTypeId], [MaxParticipants], [DateStart], [DateEnd]) VALUES
(1, 1, 20, '2026-12-01 10:00:00', '2026-12-01 11:00:00'),
(2, 2, 15, '2026-12-01 11:30:00', '2026-12-01 12:30:00'),
(3, 3, 12, '2026-12-02 09:00:00', '2026-12-02 10:00:00'),
(4, 4, 25, '2026-12-02 17:00:00', '2026-12-02 18:30:00'),
(5, 5, 18, '2026-12-03 08:30:00', '2026-12-03 09:30:00'),
(6, 6, 22, '2026-12-03 18:00:00', '2026-12-03 19:00:00'),
(7, 7, 16, '2026-12-04 12:00:00', '2026-12-04 13:00:00'),
(8, 8, 20, '2026-12-04 19:30:00', '2026-12-04 20:30:00');
SET IDENTITY_INSERT [Activities] OFF;
GO

-- 4. Asignar Monitores a Actividades (Tabla intermedia M:N ActivityInstructor)
INSERT INTO [ActivityInstructor] ([ActivitiesId], [InstructorsId]) VALUES
-- Actividad 1 (BodyPump, requiere 2 monitores)
(1, 1),
(1, 2),
-- Actividad 2 (Spinning, requiere 1 monitor)
(2, 3),
-- Actividad 3 (Pilates, requiere 1 monitor)
(3, 2),
-- Actividad 4 (CrossFit, requiere 3 monitores)
(4, 1),
(4, 3),
(4, 5),
-- Actividad 5 (Yoga, requiere 1 monitor)
(5, 6),
-- Actividad 6 (Zumba, requiere 2 monitores)
(6, 4),
(6, 8),
-- Actividad 7 (Aquagym, requiere 2 monitores)
(7, 5),
(7, 7),
-- Actividad 8 (Boxeo, requiere 2 monitores)
(8, 3),
(8, 7);
GO