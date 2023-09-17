user level score created_at updated_at

-- 테이블 생성 명령어
CREATE TABLE ranking (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_name VARCHAR(255),
    `level` INT NOT NULL,
    score INT NOT NULL,
    created_at datetime(6) NOT NULL DEFAULT current_timestamp(6),
    updated_at datetime(6) NOT NULL DEFAULT current_timestamp(6) on UPDATE current_timestamp(6)
)CHARSET=utf8mb4;