CREATE ROLE mublog_user
    WITH LOGIN
    PASSWORD 'YOUR_PASSWORD';

CREATE DATABASE mublog
    OWNER mublog_user;

-- Create PG Crypto Extension on mublog database
CREATE EXTENSION pgcrypto;
