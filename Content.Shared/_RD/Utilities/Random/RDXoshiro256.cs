using JetBrains.Annotations;

namespace Content.Shared._RD.Utilities.Random;

/// <remarks>
/// Period 2 ^ 256 - 1.
/// </remarks>
public struct RDXoshiro256
{
    private ulong _s0, _s1, _s2, _s3;

    /// <summary>
    /// Initialization of the generator from a 32-bit seed.
    /// </summary>
    public RDXoshiro256(long seed)
    {
        // Use SplitMix64 to initialize states
        _s0 = SplitMix64((ulong) seed);
        _s1 = SplitMix64(_s0);
        _s2 = SplitMix64(_s1);
        _s3 = SplitMix64(_s2);

        // Additional mixing to eliminate correlations
        for (var i = 0; i < 8; i++)
        {
            NextULong();
        }
    }

    /// <summary>
    /// Generation of float in the range [0, 1].
    /// </summary>
    [PublicAPI]
    public float NextFloat()
    {
        // We use 23 bits of the mantissa (IEEE 754 standard)
        return (NextULong() >> 40) * (1.0f / ((1 << 24) - 1));
    }

    [PublicAPI]
    public float NextFloat(float min, float max)
    {
        if (max < min)
            return min;

        var range = max - min;
        return min + NextFloat() * range;
    }

    /// <summary>
    /// Generation of int in the range [min, max].
    /// </summary>
    public int NextInt(int min, int max)
    {
        if (max <= min)
            return min;

        var range = (ulong) (max - min);
        return min + (int) (NextULong() % range);
    }

    /// <summary>
    /// Generation of int in the range [0, max].
    /// </summary>
    [PublicAPI]
    public int NextInt(int max)
    {
        return NextInt(0, max);
    }

    [PublicAPI]
    public int NextInt()
    {
        return (int) NextULong();
    }

    /// <summary>
    /// Main generation function (xoshiro256** algorithm).
    /// </summary>
    private ulong NextULong()
    {
        var result = Rotl(_s1 * 5, 7) * 9;
        var t = _s1 << 17;

        _s2 ^= _s0;
        _s3 ^= _s1;
        _s1 ^= _s2;
        _s0 ^= _s3;
        _s2 ^= t;
        _s3 = Rotl(_s3, 45);

        return result;
    }

    /// <summary>
    /// Auxiliary function - cyclic shift to the left.
    /// </summary>
    private static ulong Rotl(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }

    /// <summary>
    /// SplitMix64 for state initialization.
    /// </summary>
    private static ulong SplitMix64(ulong x)
    {
        x += 0x9E3779B97F4A7C15;
        x = (x ^ (x >> 30)) * 0xBF58476D1CE4E5B9;
        x = (x ^ (x >> 27)) * 0x94D049BB133111EB;
        return x ^ (x >> 31);
    }
}
