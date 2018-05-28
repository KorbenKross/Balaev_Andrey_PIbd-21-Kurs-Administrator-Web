CREATE TABLE Detail (
  detail_id          int NOT NULL UNIQUE, 
  detail_name        char(25) NOT NULL, 
  type               char(25) NULL, 
  [Car-kitcarkit_id] int NOT NULL, 
  PRIMARY KEY (detail_id, 
  detail_name));
ALTER TABLE Detail ADD CONSTRAINT FKDetail89255 FOREIGN KEY ([Car-kitcarkit_id]) REFERENCES [Car-kit] (carkit_id);
