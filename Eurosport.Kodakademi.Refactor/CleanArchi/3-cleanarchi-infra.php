<?php
namespace Infrastructure\App\Services;

use App\Services\ImageRepository;
use Ramsey\Uuid\Uuid;
use Aws\S3\S3Client;
use League\Flysystem\AwsS3v3\AwsS3Adapter;
use League\Flysystem\Filesystem;

class S3ImageRepository implements ImageRepository
{
    private $filesystem;

    public function __construct()
    {
            $client = S3Client::factory([
                'credentials' => [
                    'key' => getenv('AWS_ACCESS_KEY'),
                    'secret' => getenv('AWS_SECRET'),
                ],
                'region' => getenv('AWS_REGION'),
                'version' => 'latest',
            ]);

            $adapter = new AwsS3Adapter($client, 'bucket-o-images');

            $this->filesystem = new Filesystem($adapter);
    }

    public function store(Uuid $image_id, SplFileInfo $image)
    {
        $filepath = "profle_image/$image_id.".$file->getExtension()
        $image_contents = $image->fread($image->getSize());
        $this->filesystem->write($filepath, $image_contents);
    }
}

namespace Infrastructure\App\Services;

use App\Services\UserRepostiory;
use Ramsey\Uuid\Uuid;
use Domain\User;

class EloquentUserRepostiory implements UserRepostiory
{
    public function get(Uuid $user_id):User 
    {
        return User::find($user_id->toString());
    }

    public function store($user)
    {
        User::where('id', $user_id->toString())->update($user->toArray());
    }
}