CREATE ROLE mublog_user
    WITH LOGIN
    PASSWORD 'YOUR_PASSWORD';

CREATE TABLE mublog
    OWNER 'mublog_user';