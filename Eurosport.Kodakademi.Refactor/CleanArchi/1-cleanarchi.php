<?php
namespace App\Usecases;

use Ramsey\Uuid\Uuid;
use SplFileInfo;
use Aws\S3\S3Client;
use League\Flysystem\AwsS3v3\AwsS3Adapter;
use League\Flysystem\Filesystem;
use Domain\User;

class SetProfileImage 
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

    public function handle(Uuid $user_id, SplFileInfo $image): Uuid
    {
        $image_id = Uuid::uuid4();

        $filepath = "profle_image/$image_id.".$file->getExtension()
        $image_contents = $image->fread($image->getSize());
        $this->filesystem->write($filepath, $image_contents);

        $user = User::find($user_id->toString());
        $user->setProfileImage($image_id);
        User::where('id', $user_id->toString())->update($user->toArray());

        return $image_id;
    }
}