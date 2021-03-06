﻿CREATE TABLE users
(
  ID serial NOT NULL,
  email character varying(60) NOT NULL,
  joinDate character varying(50) NOT NULL,
  averageScore real,
  CONSTRAINT "User_pkey" PRIMARY KEY (ID)
);

Create TYPE tripData as
(
  latitude character varying(12),
  longitude character varying(12),
  speed double,
  recTime character varying(5),
  maxXAcelerometer double,
  maxYAcelerometer double,
  maxZAcelerometer double
);

Create TABLE trips
(
  ID serial NOT NULL,
  userID int references users(ID),
  tripDate character varying(40),
  startLatitude character varying(11),
  startLongitude character varying(11),
  recordedData tripData[],
  CONSTRAINT "Trips_pkey" PRIMARY KEY (ID)
);

CREATE TYPE status as ENUM ('accepted','pending','blocked');

CREATE TABLE friends
(
  ID serial NOT NULL,
  userID int references users(ID),
  friendID int references users(ID),
  currentStatus status,
  CONSTRAINT "Friends_pkey" PRIMARY KEY (ID)
);

