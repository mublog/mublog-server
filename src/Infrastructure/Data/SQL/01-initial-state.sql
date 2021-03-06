START TRANSACTION;


CREATE EXTENSION pgcrypto;

CREATE TABLE profiles
(
    id               integer                     NOT NULL GENERATED ALWAYS AS IDENTITY,
    date_created     timestamp without time zone NOT NULL,
    date_updated     timestamp without time zone NOT NULL,
    username         character varying(20)       NOT NULL,
    display_name     character varying(30)       NOT NULL,
    description      text,
    profile_image_id integer,
    user_state       integer                     NOT NULL DEFAULT 0,
    CONSTRAINT pk_profiles_id PRIMARY KEY (id)
);

CREATE TABLE posts
(
    id               integer                     NOT NULL GENERATED ALWAYS AS IDENTITY,
    date_created     timestamp without time zone NOT NULL,
    date_updated     timestamp without time zone NOT NULL,
    public_id        integer                     NOT NULL UNIQUE GENERATED BY DEFAULT AS IDENTITY,
    content          text                        NOT NULL,
    owner_id         integer                     NOT NULL,
    date_post_edited timestamp without time zone NOT NULL,
    CONSTRAINT pk_posts_id PRIMARY KEY (id),
    CONSTRAINT fk_posts_id FOREIGN KEY (owner_id) REFERENCES profiles (id) ON DELETE CASCADE
);

CREATE TABLE comments
(
    id             integer                     NOT NULL GENERATED ALWAYS AS IDENTITY,
    date_created   timestamp without time zone NOT NULL,
    date_updated   timestamp without time zone NOT NULL,
    content        text                        NOT NULL,
    parent_post_id integer                     NOT NULL,
    owner_id       integer                     NOT NULL,
    CONSTRAINT pk_comments_id PRIMARY KEY (id),
    CONSTRAINT fk_comments_parent_post_id FOREIGN KEY (parent_post_id) REFERENCES posts (id) ON DELETE CASCADE,
    CONSTRAINT fk_comments_owner_id FOREIGN KEY (owner_id) REFERENCES profiles (id) ON DELETE CASCADE
);

CREATE TABLE mediae
(
    id           integer                     NOT NULL GENERATED ALWAYS AS IDENTITY,
    data_created timestamp without time zone NOT NULL,
    date_updated timestamp without time zone NOT NULL,
    post_id      integer,
    public_id    uuid                        NOT NULL,
    media_type   integer                     NOT NULL,
    owner_id     integer                     NOT NULL,
    CONSTRAINT pk_mediae_id PRIMARY KEY (id),
    CONSTRAINT fk_mediae_post_id FOREIGN KEY (post_id) REFERENCES posts (id) ON DELETE SET NULL,
    CONSTRAINT fk_mediae_owner_id FOREIGN KEY (owner_id) REFERENCES profiles (id) ON DELETE SET NULL
);

CREATE TABLE accounts
(
    id           integer                     NOT NULL GENERATED ALWAYS AS IDENTITY,
    data_created timestamp without time zone NOT NULL,
    date_updated timestamp without time zone NOT NULL,
    email        character varying(254)      NOT NULL,
    password     text                        NOT NULL,
    profile_id   integer                     NOT NULL,
    CONSTRAINT pk_accounts_id PRIMARY KEY (id),
    CONSTRAINT fk_accounts_profile_id FOREIGN KEY (profile_id) REFERENCES profiles (id) ON DELETE CASCADE
);

CREATE TABLE posts_liked_by_profiles
(
    liked_posts_id    integer NOT NULL,
    liking_profile_id integer NOT NULL,
    CONSTRAINT pk_posts_liked_by_profiles PRIMARY KEY (liked_posts_id, liking_profile_id),
    CONSTRAINT fk_posts_liked_by_profiles_liked_posts_id FOREIGN KEY (liked_posts_id) REFERENCES posts (id) ON DELETE CASCADE,
    CONSTRAINT fk_posts_liked_by_profiles_likes_id FOREIGN KEY (liking_profile_id) REFERENCES profiles (id) ON DELETE CASCADE
);

CREATE TABLE profiles_following_profile
(
    follower_id  integer NOT NULL,
    following_id integer NOT NULL,
    CONSTRAINT pk_profiles_following_profile PRIMARY KEY (follower_id, following_id),
    CONSTRAINT fk_profiles_following_profile_follower_id FOREIGN KEY (follower_id) REFERENCES profiles (id) ON DELETE CASCADE,
    CONSTRAINT fk_profiles_following_profile_following_id FOREIGN KEY (following_id) REFERENCES profiles (id) ON DELETE CASCADE
);


ALTER TABLE profiles
    ADD CONSTRAINT fk_profile_image_id FOREIGN KEY (profile_image_id) REFERENCES mediae (id);


CREATE UNIQUE INDEX ix_profiles_username ON profiles (username);

-- CREATE INDEX ix_posts_owner_id ON posts (owner_id);
CREATE INDEX ix_posts_public_id ON posts (public_id);

CREATE INDEX ix_comments_parent_post_id ON comments (parent_post_id);

CREATE INDEX ix_mediae_owner_id ON mediae (owner_id);
CREATE INDEX ix_mediae_post_id ON mediae (post_id);

CREATE INDEX ix_accounts_profile_id ON accounts (profile_id);

CREATE INDEX ix_profiles_following_profile_follower_id ON profiles_following_profile (follower_id);
CREATE INDEX ix_profiles_following_profile_following_id ON profiles_following_profile (following_id);

CREATE INDEX ix_post_profiles_likes_id ON posts_liked_by_profiles (liking_profile_id);
CREATE INDEX ix_post_profiles_liked_posts_id ON posts_liked_by_profiles (liked_posts_id);


COMMIT;