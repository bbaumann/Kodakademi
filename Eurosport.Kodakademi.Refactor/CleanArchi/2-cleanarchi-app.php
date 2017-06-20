<?php

namespace App\Usecases;

use Ramsey\Uuid\Uuid;
use SplFileInfo;

class SetProfileImage 
{
    private $image_store;
    private $user_repository;

    public function __construct(ImageRepo $image_repo, UserRepository $user_repository)
    {
        $this->image_repo = image_repo;
        $this->user_repository = $user_repository;
    }

    public function handle(Uuid $user_id, SplFileInfo $image): Uuid
    {
        $image_id = Uuid::uuid4();

        $this->image_repo->store($image_id, $image);

        $user = $this->user_repository->get($user_id);

        $user->setProfileImage($image_id);

        $user_repository->store($user);

        return $image_id;
    }
}

namespace App\Services;

use Ramsey\Uuid\Uuid;
use SplFileInfo;

interface ImageRepository 
{
    public function store(Uuid $image_id, SplFileInfo $image);
}

namespace App\Services;

use Uuid;
use Domain\User;

interface UserRepostiory
{
    public function get(Uuid $user_id): User

    public function store($user);
}